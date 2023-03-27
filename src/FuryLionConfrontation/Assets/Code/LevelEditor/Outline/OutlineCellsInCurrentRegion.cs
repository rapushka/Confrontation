using System.Collections.Generic;
using System.Linq;
using cakeslice;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OutlineCellsInCurrentRegion : ITickable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly LevelEditorTabsSystem _tabs;

		private const int MaxLastRegionsToStore = 2;

		private readonly Queue<Region> _lastSelectedRegions = new(capacity: MaxLastRegionsToStore);
		private Region _currentSelectedRegion;

		private bool _isDisabled;

		public void Tick()
		{
			if (_tabs.CurrentPage is SelectableListPage<RegionEntry> { HasSelectedEntry: true } tab)
			{
				KeepTwoRegionsInQueue(tab.SelectedEntry.Region);
				DrawOutlines();
				_isDisabled = false;
				return;
			}

			if (_isDisabled == false)
			{
				_field.Cells.ForEach(RemoveOutline);
				_isDisabled = true;
			}
		}

		private void KeepTwoRegionsInQueue(Region selectedRegion)
		{
			if (_currentSelectedRegion == selectedRegion)
			{
				return;
			}

			_lastSelectedRegions.Enqueue(_currentSelectedRegion);

			if (_lastSelectedRegions.Count > MaxLastRegionsToStore)
			{
				_lastSelectedRegions.Dequeue();
			}

			_currentSelectedRegion = selectedRegion;

			_field.Cells.ForEach(RemoveOutline);
		}

		private void DrawOutlines()
		{
			var selectedRegions = _lastSelectedRegions.Reverse().ToArray();

			foreach (var cell in _field.Cells)
			{
				if (cell.RelatedRegion == _currentSelectedRegion)
				{
					AddOutline(cell, 0);
					continue;
				}

				if (DrawForPreviousRegions(selectedRegions, cell) == false)
				{
					RemoveOutline(cell);
				}
			}
		}

		private static bool DrawForPreviousRegions(Region[] selectedRegions, Cell cell)
		{
			var painted = false;
			for (var i = 0; i < selectedRegions.Length; i++)
			{
				if (cell.RelatedRegion == selectedRegions[i])
				{
					AddOutline(cell, i + 1);
					painted = true;
					break;
				}
			}

			return painted;
		}

		private static void AddOutline(Cell cell, int color)
		{
			var mesh = cell.GetComponentInChildren<MeshRenderer>().gameObject;
			if (mesh.TryGetComponent(out Outline outline) == false)
			{
				outline = mesh.AddComponent<Outline>();
			}

			outline.color = color;
		}

		private static void RemoveOutline(Cell cell)
		{
			var mesh = cell.GetComponentInChildren<MeshRenderer>().gameObject;
			if (mesh.TryGetComponent(out Outline outline))
			{
				Object.Destroy(outline);
			}
		}
	}
}
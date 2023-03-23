using System.Collections.Generic;
using cakeslice;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OutlineCellsInCurrentRegion : ITickable
	{
		private const int MaxRegionsInQueue = 2;
		[Inject] private readonly IField _field;
		[Inject] private readonly LevelEditorTabsSystem _tabs;

		private readonly Queue<Region> _lastSelectedRegions = new(capacity: MaxRegionsInQueue);

		public void Tick()
		{
			if (_tabs.CurrentPage is not RegionsTab tab
			    || tab.SelectedEntry == false)
			{
				return;
			}

			var selectedRegion = tab.SelectedEntry.Region;

			KeepTwoRegionsInQueue(selectedRegion);

			DrawOutlines(selectedRegion);
		}

		private void KeepTwoRegionsInQueue(Region selectedRegion)
		{
			if (_lastSelectedRegions.Contains(selectedRegion))
			{
				return;
			}

			_lastSelectedRegions.Enqueue(selectedRegion);

			if (_lastSelectedRegions.Count <= MaxRegionsInQueue)
			{
				return;
			}

			var oldestRegion = _lastSelectedRegions.Dequeue();
			foreach (var cell in _field.Cells)
			{
				if (cell.RelatedRegion == oldestRegion)
				{
					RemoveOutline(cell);
				}
			}
		}

		private void DrawOutlines(Region selectedRegion)
		{
			foreach (var cell in _field.Cells)
			{
				if (cell.RelatedRegion == selectedRegion)
				{
					AddOutline(cell, 0);
				}
				else if (_lastSelectedRegions.Contains(cell.RelatedRegion))
				{
					AddOutline(cell, 1);
				}
				else
				{
					RemoveOutline(cell);
				}
			}
		}

		private static void AddOutline(Cell cell, int color)
		{
			var mesh = cell.GetComponentInChildren<MeshRenderer>().gameObject;
			if (mesh.TryGetComponent(out Outline _) == false)
			{
				var outline = mesh.AddComponent<Outline>();
				outline.color = color;
			}
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
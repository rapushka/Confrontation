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

		private const int MaxRegionsInQueue = 2;

		private readonly Queue<Region> _lastSelectedRegions = new(capacity: MaxRegionsInQueue);

		private Region _currentSelectedRegion;
		private Region _lastSelectedRegion;

		public void Tick()
		{
			if (_tabs.CurrentPage is not RegionsTab tab
			    || tab.SelectedEntry == false)
			{
				return;
			}

			KeepTwoRegionsInQueue(tab.SelectedEntry.Region);
			DrawOutlines();
		}

		private void KeepTwoRegionsInQueue(Region selectedRegion)
		{
			if (_currentSelectedRegion != selectedRegion)
			{
				_lastSelectedRegion = _currentSelectedRegion;
				_currentSelectedRegion = selectedRegion;
				
				foreach (var cell in _field.Cells)
				{
					RemoveOutline(cell);
				}
			}
		}

		private void DrawOutlines()
		{
			foreach (var cell in _field.Cells)
			{
				if (cell.RelatedRegion == _currentSelectedRegion)
				{
					AddOutline(cell, 0);
					continue;
				}
				
				if (cell.RelatedRegion == _lastSelectedRegion)
				{
					AddOutline(cell, 1);
					continue;
				}

				RemoveOutline(cell);
			}
		}

		private void Print(Region region, string name)
		{
			if (region is not null)
			{
				Debug.Log($"[{name}] {region.Id} — {region.Id.GetHashCode()}");
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
using cakeslice;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class OutlineCellsInCurrentRegion : ITickable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly LevelEditorTabsSystem _tabs;

		public void Tick()
		{
			if (_tabs.CurrentPage is not RegionsTab tab
			    || tab.SelectedEntry == false)
			{
				return;
			}

			foreach (var cell in _field.Cells)
			{
				if (cell.RelatedRegion == tab.SelectedEntry.Region)
				{
					AddOutline(cell);
				}
				else
				{
					RemoveOutline(cell);
				}
			}
		}

		private static void AddOutline(Cell cell)
		{
			var mesh = cell.GetComponentInChildren<MeshRenderer>().gameObject;
			if (mesh.TryGetComponent(out Outline _) == false)
			{
				mesh.AddComponent<Outline>();
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
using cakeslice;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class OutlineCellsInCurrentRegion : ITickable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly LevelEditorTabsSystem _tabs;

		private Region _selectedRegion;

		public void Tick() => _field.Cells.ForEach(IsRegionSelected() ? DrawSelectedRegion : RemoveOutline);

		private bool IsRegionSelected()
		{
			if (_tabs.CurrentPage is IRegionSelector { HasSelectedEntry: true } regionsOwnershipPage)
			{
				_selectedRegion = regionsOwnershipPage.SelectedRegion;
				return true;
			}

			_selectedRegion = null;
			return false;
		}

		private void DrawSelectedRegion(Cell cell)
		{
			if (cell.RelatedRegion == _selectedRegion)
			{
				AddOutline(cell);
			}
			else
			{
				RemoveOutline(cell);
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
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor
{
	public class BuildingsCreator : IGuiRenderable
	{
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IResourcesService _resources;

		private static IEnumerable<Cell> SelectedCells => Selection.gameObjects.WithComponent<Cell>();

		public void GuiRender()
		{
			GUILayout.Label("Actions Perform to Selected Cells");
			GUILayout.Button(nameof(BuildCapital).Pretty()).OnClick(BuildCapital);
			GUILayout.Button(nameof(DestroyBuilding).Pretty()).OnClick(DestroyBuilding);
		}

		private void BuildCapital() => SelectedCells.Where((c) => c.IsEmpty).ForEach(CreateCapitalOnCell);

		private void DestroyBuilding() => SelectedCells.Where((c) => c.IsEmpty == false).ForEach(DestroyBuildingOnCell);

		private void CreateCapitalOnCell(Cell cell)
		{
			var capital = _assets.Instantiate(_resources.CapitalPrefab, cell.transform);
			capital.RelatedCell = cell;
			cell.Building = capital;
		}

		private void DestroyBuildingOnCell(Cell cell)
		{
			var building = cell.Building!;
			_assets.Destroy(building.gameObject);
			cell.Building = null;
		}
	}
}
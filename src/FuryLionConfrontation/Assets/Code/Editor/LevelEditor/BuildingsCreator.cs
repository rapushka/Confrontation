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
			GUILayout.Button(nameof(BuildVillage).Pretty()).OnClick(BuildVillage);
			GUILayout.Button(nameof(DestroyBuilding).Pretty()).OnClick(DestroyBuilding);
		}

		private void BuildCapital() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildCapitalOnCell);

		private void BuildVillage() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildVillageOnCell);

		private void DestroyBuilding() => SelectedCells.Where((c) => c.IsEmpty == false).ForEach(DestroyBuildingOnCell);

		private void BuildCapitalOnCell(Cell cell) => BuildBuilding(cell, _resources.CapitalPrefab);

		private void BuildVillageOnCell(Cell cell) => BuildBuilding(cell, _resources.VillagePrefab);

		private void DestroyBuildingOnCell(Cell cell)
		{
			var building = cell.Building!;
			_assets.Destroy(building.gameObject);
			cell.Building = null;
		}

		private void BuildBuilding(Cell cell, Building buildingPrefab)
		{
			var building = _assets.Instantiate(buildingPrefab, cell.transform);
			building.RelatedCell = cell;
			cell.Building = building;
		}
	}
}
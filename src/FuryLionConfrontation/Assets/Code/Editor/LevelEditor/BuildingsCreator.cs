using System;
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

		public event Action<Building> BuildingAdd;
		public event Action<Building> BuildingRemove;

		private static IEnumerable<Cell> SelectedCells => Selection.gameObjects.WithComponent<Cell>();

		private Barracks BarrackPrefab => _resources.Buildings.OfType<Barracks>().Single();

		private GoldenMine GoldenMinePrefab => _resources.Buildings.OfType<GoldenMine>().Single();

		public void GuiRender()
		{
			GUILayout.Label("Actions Perform to Selected Cells");
			GUILayout.Button(nameof(BuildCapital).Pretty()).OnClick(BuildCapital);
			GUILayout.Button(nameof(BuildVillage).Pretty()).OnClick(BuildVillage);
			GUILayout.Button(nameof(BuildBarrack).Pretty()).OnClick(BuildBarrack);
			GUILayout.Button(nameof(BuildGoldenMine).Pretty()).OnClick(BuildGoldenMine);
			GUILayout.Button(nameof(DestroyBuilding).Pretty()).OnClick(DestroyBuilding);
		}

		private void BuildCapital() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildCapitalOnCell);

		private void BuildVillage() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildVillageOnCell);

		private void BuildBarrack() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildBarrackOnCell);

		private void BuildGoldenMine() => SelectedCells.Where((c) => c.IsEmpty).ForEach(BuildGoldenMineOnCell);

		private void DestroyBuilding() => SelectedCells.Where((c) => c.IsEmpty == false).ForEach(DestroyBuildingOnCell);

		private void BuildCapitalOnCell(Cell cell) => BuildBuilding(cell, _resources.CapitalPrefab);

		private void BuildVillageOnCell(Cell cell) => BuildBuilding(cell, _resources.VillagePrefab);

		private void BuildBarrackOnCell(Cell cell) => BuildBuilding(cell, BarrackPrefab);

		private void BuildGoldenMineOnCell(Cell cell) => BuildBuilding(cell, GoldenMinePrefab);

		private void DestroyBuildingOnCell(Cell cell)
		{
			var building = cell.Building!;
			BuildingRemove?.Invoke(building);

			BuildingsStorage.Buildings.Remove(building);
			_assets.Destroy(building.gameObject);
			cell.Building = null;
		}

		private void BuildBuilding(Cell cell, Building buildingPrefab)
		{
			var building = _assets.Instantiate(buildingPrefab, cell.transform);
			BuildingAdd?.Invoke(building);
			building.Coordinates = cell.Coordinates;
		}
	}
}
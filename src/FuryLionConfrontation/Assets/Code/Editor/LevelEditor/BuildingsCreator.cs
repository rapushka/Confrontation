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
		[Inject] private readonly IField _field;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IResourcesService _resources;

		public event Action<Building> BuildingAdd;
		public event Action<Building> BuildingRemove;

		private static IEnumerable<Cell> SelectedCells => Selection.gameObjects.WithComponent<Cell>();

		private Barrack BarrackPrefab => _resources.Buildings.OfType<Barrack>().Single();

		private GoldenMine GoldenMinePrefab => _resources.Buildings.OfType<GoldenMine>().Single();

		public void GuiRender()
		{
			GUILayout.Label("Actions Perform to Selected Cells");
			GUILayout.Button("Build Capital").OnClick(() => Build(_resources.CapitalPrefab));
			GUILayout.Button("Build Village").OnClick(() => Build(_resources.VillagePrefab));
			GUILayout.Button("Build Barrack").OnClick(() => Build(BarrackPrefab));
			GUILayout.Button("Build Golden Mine").OnClick(() => Build(GoldenMinePrefab));
			GUILayout.Button(nameof(DestroyBuilding).Pretty()).OnClick(DestroyBuilding);
		}

		private void Build(Building prefab)
			=> SelectedCells.Where((c) => c.IsEmpty).ForEach((c) => BuildOnCell(c, prefab));
		private void DestroyBuilding() => SelectedCells.Where((c) => c.IsEmpty == false).ForEach(DestroyBuildingOnCell);

		private void BuildOnCell(Cell cell, Building prefab)
		{
			var building = _assets.Instantiate(prefab, cell.transform);
			BuildingAdd?.Invoke(building);
			building.Coordinates = cell.Coordinates;
		}

		private void DestroyBuildingOnCell(Cell cell)
		{
			var building = _field.Buildings[cell.Coordinates];

			BuildingRemove?.Invoke(building);
			_field.Buildings.Remove(building);

			_assets.Destroy(building.gameObject);
		}
	}
}
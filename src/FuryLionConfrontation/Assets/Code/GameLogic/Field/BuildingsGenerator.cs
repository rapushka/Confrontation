using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class BuildingsGenerator : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly IResourcesService _resourcesService;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		private GoldenMine GoldenMinePrefab => _resourcesService.GoldenMinePrefab;

		public void Initialize() => GenerateBuildings();

		private void GenerateBuildings() => _levelSelector.SelectedLevel.Buildings.ForEach(Create);

		private void Create(Building.Data data)
			=> _buildingsFactory.Create(GoldenMinePrefab, OwnerCell(data), data.OwnerPlayerId);

		private Transform OwnerCell(Building.Data data) => _field.Cells[data.Coordinates].transform;
	}
}
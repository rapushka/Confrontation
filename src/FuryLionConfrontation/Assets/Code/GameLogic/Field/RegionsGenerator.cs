using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class RegionsGenerator : IInitializable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IResourcesService _resourcesService;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly BuildingsGenerator _buildingsGenerator;

		private Village VillagePrefab => _resourcesService.VillagePrefab;

		public List<Village> Villages { get; } = new();

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _levelSelector.SelectedLevel.Regions.ForEach(ToRegion);

		private void ToRegion(RegionData regionData)
		{
			var village = CreateVillage(regionData);
			GetCellsFrom(regionData).ForEach(village.AddToRegion);
			Villages.Add(village);
			_buildingsGenerator.Buildings.Add(village);
		}

		private Village CreateVillage(RegionData regionData)
		{
			var ownerCell = _field.Cells[regionData.VillageCoordinates];
			var village = Create(regionData, ownerCell);
			_field.Buildings.Add(village);
			return village;
		}

		private IEnumerable<Cell> GetCellsFrom(RegionData regionData) => regionData.CellsCoordinates.Select((c) => _field.Cells[c]);

		private Village Create(RegionData regionData, Cell ownerCell)
			=> _buildingsFactory.Create(VillagePrefab, ownerCell, regionData.OwnerPlayerId);
	}
}
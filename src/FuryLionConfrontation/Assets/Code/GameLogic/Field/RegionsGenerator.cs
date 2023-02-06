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

		private void ToRegion(Region region)
		{
			var village = CreateVillage(region);
			GetCellsFrom(region).ForEach(village.AddToRegion);
			Villages.Add(village);
			_buildingsGenerator.Buildings.Add(village);
		}

		private Village CreateVillage(Region region)
		{
			var ownerCell = _field.Cells[region.VillageCoordinates];
			var village = Create(region, ownerCell);
			_field.Buildings.Add(village);
			return village;
		}

		private IEnumerable<Cell> GetCellsFrom(Region region) => region.CellsCoordinates.Select((c) => _field.Cells[c]);

		private Village Create(Region region, Cell ownerCell)
			=> _buildingsFactory.Create(VillagePrefab, ownerCell, region.OwnerPlayerId);
	}
}
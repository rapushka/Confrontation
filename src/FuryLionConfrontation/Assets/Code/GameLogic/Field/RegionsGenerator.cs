using Zenject;

namespace Confrontation
{
	public class RegionsGenerator : IInitializable
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly IResourcesService _resourcesService;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Building.Factory _buildingsFactory;
		[Inject] private readonly Region.Factory _regionsFactory;

		private Village VillagePrefab => _resourcesService.VillagePrefab;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _levelSelector.SelectedLevel.Regions.ForEach(ToRegion);

		private void ToRegion(RegionData regionData)
		{
			var region = _regionsFactory.Create(regionData);
			var village = CreateVillage(regionData);

			foreach (var coordinates in regionData.CellsCoordinates)
			{
				_field.Regions[coordinates] = region;
			}

			region.UpdateCellsColor();
			_field.Buildings.Add(village);
		}

		private Village CreateVillage(RegionData regionData)
		{
			var ownerCell = _field.Cells[regionData.VillageCoordinates];
			var village = Create(ownerCell);
			return village;
		}

		private Village Create(Cell ownerCell)
			=> _buildingsFactory.Create(VillagePrefab, ownerCell);
	}
}
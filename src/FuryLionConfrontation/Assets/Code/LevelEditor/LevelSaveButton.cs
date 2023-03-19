using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class LevelSaveButton : ButtonBase
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly ILevelSaver _levelSaver;

		protected override void OnButtonClick()
		{
			var regionsData = RegionsToData();
			var buildingsData = BuildingsToData();

			var level = new ImmutableLevel(_field.Cells.Sizes, regionsData, buildingsData);
			_levelSaver.Save(level);
		}

		private List<Region.Data> RegionsToData()
		{
			var regionsDictionary = new Dictionary<int, Region.Data>();
			foreach (var fieldRegion in _field.Regions.OfType<Region>())
			{
				regionsDictionary.EnsureAdded(fieldRegion.Id, ToRegionData(fieldRegion));
				regionsDictionary[fieldRegion.Id].CellsCoordinates.Add(fieldRegion.Coordinates);
			}

			return regionsDictionary.Values.ToList();
		}

		private List<Building.CoordinatedData> BuildingsToData()
			=> _field.Buildings.OfType<Building>().Select(ToBuildingData).ToList();

		private static Building.CoordinatedData ToBuildingData(Building building)
			=> new()
			{
				Coordinates = building.Coordinates,
				Prefab = BuildingsCollection.Load(building.GetType().Name),
			};

		private static Region.Data ToRegionData(Region region)
			=> new()
			{
				OwnerPlayerId = region.OwnerPlayerId,
				CellsCoordinates = new List<Coordinates>(),
			};
	}
}
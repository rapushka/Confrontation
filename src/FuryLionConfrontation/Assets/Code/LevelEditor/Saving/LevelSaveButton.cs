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
			var level = new ImmutableLevel(_field.Cells.Sizes, RegionsToData(), BuildingsToData());
			_levelSaver.Save(level);
		}

		private List<Region.Data> RegionsToData()
			=> _field.Regions.WithoutNulls().OnlyUnique().Select(AsData).ToList();

		private Region.Data AsData(Region region)
			=> new(region.OwnerPlayerId, GetCellsInRegion(region));

		private static List<Coordinates> GetCellsInRegion(Region region)
			=> region.CellsInRegion.Select((c) => c.Coordinates).ToList();

		private List<Building.CoordinatedData> BuildingsToData()
			=> _field.Buildings.WithoutNulls().Select(ToBuildingData).ToList();

		private static Building.CoordinatedData ToBuildingData(Building building)
			=> new()
			{
				Coordinates = building.Coordinates,
				Prefab = BuildingsCollection.Load(building.GetType().Name),
			};
	}
}
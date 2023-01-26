using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Regions : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly ILevelSelector _levelSelector;
		[Inject] private readonly Village.Factory _villageFactory;

		public void Initialize() => DivideIntoRegions();

		private void DivideIntoRegions() => _levelSelector.SelectedLevel.Regions.ForEach(ToRegion);

		private void ToRegion(Region region)
		{
			var village = CreateVillage(region);
			GetCellsFrom(region).ForEach(village.AddToRegion);
		}

		private IEnumerable<Cell> GetCellsFrom(Region region) => region.CellsCoordinates.Select((c) => _field.Cells[c]);

		private Village CreateVillage(Region region)
		{
			var ownerCell = _field.Cells[region.VillageCoordinates];
			var village = _villageFactory.Create(ownerCell, region.OwnerPlayerId);
			ownerCell.Building = village;
			return village;
		}
	}
}
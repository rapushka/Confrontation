using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Our
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly Player _player;
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IBalanceTable _balanceTable;

		public UnitsSquad[] Units => _field.LocatedUnits.Where(IsOurUnit).AsArray();

		public IEnumerable<Cell> EmptyCells => _field.Cells.Where((c) => c.OwnerPlayerId == _player.Id && c.IsEmpty);

		public IEnumerable<Building> CanBeBoughtBuildings
			=> _resources.Buildings.Where((b) => _player.Stats.IsEnoughGoldFor(_balanceTable.PriceFor(b)));

		public IEnumerable<Region> Regions => _field.Regions.Where((r) => r.OwnerPlayerId == _player.Id).OnlyUnique();

		private bool IsOurUnit(UnitsSquad unit) => unit is not null && unit.OwnerPlayerId == _player.Id;

		public IEnumerable<Village> NeighboursFor(UnitsSquad randomSquad)
			=> _field.Buildings
			         .OfType<Village>()
			         .Where((v) => IsOnNeighbourRegions(randomSquad, v));

		private bool IsOnNeighbourRegions(UnitsSquad squad, Building village)
			=> IsNeighbours(squad.LocationCell.RelatedRegion, village.RelatedCell.RelatedRegion);

		private bool IsNeighbours(Region currentRegion, Region targetRegion)
			=> _field.Neighboring.IsNeighbours(targetRegion, currentRegion)
			   && targetRegion != currentRegion;
	}
}
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class Our
	{
		[Inject] private readonly IField _field;
		[Inject] private readonly Player _player;
		[Inject] private readonly IBalanceTable _balanceTable;

		public UnitsSquad[] Units => _field.LocatedUnits.Where(IsOurUnit).AsArray();

		public IEnumerable<Cell> EmptyCells => _field.Cells.Where((c) => c.OwnerPlayerId == _player.Id && c.IsEmpty);

		public IEnumerable<Region> Regions => _field.Regions.Where((r) => r.OwnerPlayerId == _player.Id).OnlyUnique();

		public bool CanBuyPreferredBuilding(out Building building)
		{
			building = _balanceTable.EnemiesStats.BuildingsPriority.PickRandom().Prefab;
			return _player.Resources.Gold.IsEnoughFor(_balanceTable.BuildPriceFor(building));
		}

		public IEnumerable<IPlaceable> NeighboursFor(Cell cell) =>
			_field.Buildings
			      .OfType<IPlaceable>()
			      .Where((v) => IsNeighbours(cell.RelatedRegion, v.RelatedCell.RelatedRegion));

		private bool IsOurUnit(UnitsSquad unit) => unit is not null && unit.OwnerPlayerId == _player.Id;

		private bool IsNeighbours(Region currentRegion, Region targetRegion) =>
			_field.Neighborhoods.IsNeighbours(targetRegion, currentRegion)
			&& targetRegion != currentRegion;
	}
}
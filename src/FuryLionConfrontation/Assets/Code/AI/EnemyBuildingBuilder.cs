using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class EnemyBuildingBuilder
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IField _field;
		[Inject] private readonly Purchase _purchase;
		[Inject] private readonly IResourcesService _resources;
		[Inject] private readonly IBalanceTable _balanceTable;

		private IEnumerable<Cell> OurEmptyCells
			=> _field.Cells.Where((c) => c.OwnerPlayerId == _player.Id).Where((c) => c.IsEmpty);

		private IEnumerable<Building> BuildingsThatWeCanBuy
			=> _resources.Buildings.Where((b) => _player.Stats.IsEnoughGoldFor(_balanceTable.PriceFor(b)));

		public void Build()
		{
			if (BuildingsThatWeCanBuy.TryPickRandom(out var building)
			    && OurEmptyCells.TryPickRandom(out var emptyCell))
			{
				_purchase.BuyBuilding(_player, building, emptyCell);
			}
		}

		public class Factory : PlaceholderFactory<Player, EnemyBuildingBuilder> { }
	}
}
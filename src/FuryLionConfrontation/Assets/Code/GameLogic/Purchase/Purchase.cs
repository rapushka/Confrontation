using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;
		[Inject] private readonly IBalanceTable _balanceTable;

		public bool BuyBuilding(Player userPlayer, Building building, Cell locationCell)
		{
			var buildingPrice = _balanceTable.PriceFor(building);

			if (userPlayer.Stats.IsEnoughGoldFor(buildingPrice))
			{
				_buildingSpawner.Build(building, locationCell);
				userPlayer.Stats.Spend(buildingPrice);
				return true;
			}

			return false;
		}
	}
}
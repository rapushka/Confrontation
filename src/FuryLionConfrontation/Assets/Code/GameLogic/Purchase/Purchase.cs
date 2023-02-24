using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly IBalanceTable _balanceTable;
		[Inject] private readonly BuildingSpawner _buildingSpawner;

		public bool BuyBuilding(Player userPlayer, Building building)
		{
			var buildingPrice = _balanceTable.PriceFor(building);

			if (userPlayer.Stats.IsEnoughGoldFor(buildingPrice))
			{
				_buildingSpawner.Build(building);
				userPlayer.Stats.Spend(buildingPrice);
				return true;
			}

			return false;
		}
	}
}
using Zenject;

namespace Confrontation
{
	public class Purchase : IPurchase
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;
		[Inject] private readonly IBalanceTable _balanceTable;

		public bool BuyBuilding(Player player, Building buildingPrefab, Cell locationCell)
		{
			var buildingPrice = _balanceTable.BuildPriceFor(buildingPrefab);

			if (player.Resources.Gold.IsEnoughGoldFor(buildingPrice))
			{
				_buildingSpawner.Build(buildingPrefab, locationCell);
				player.Resources.Gold.Spend(buildingPrice);
				return true;
			}

			return false;
		}

		public bool UpgradeBuilding(Player player, Building building)
		{
			var upgradePrice = building.UpgradePrice;

			if (player.Resources.Gold.IsEnoughGoldFor(upgradePrice))
			{
				building.LevelUp();
				player.Resources.Gold.Spend(upgradePrice);
				return true;
			}

			return false;
		}
	}
}
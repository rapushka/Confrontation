using Zenject;

namespace Confrontation
{
	public class Purchase
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;
		[Inject] private readonly IBalanceTable _balanceTable;

		public bool BuyBuilding(Player player, Building buildingPrefab, Cell locationCell)
		{
			var buildingPrice = _balanceTable.BuildPriceFor(buildingPrefab);

			if (player.Stats.IsEnoughGoldFor(buildingPrice))
			{
				_buildingSpawner.Build(buildingPrefab, locationCell);
				player.Stats.Spend(buildingPrice);
				return true;
			}

			return false;
		}

		public bool UpgradeBuilding(Player player, Building building)
		{
			var upgradePrice = building.UpgradePrice;

			if (player.Stats.IsEnoughGoldFor(upgradePrice))
			{
				building.LevelUp();
				player.Stats.Spend(upgradePrice);
				return true;
			}

			return false;
		}
	}
}
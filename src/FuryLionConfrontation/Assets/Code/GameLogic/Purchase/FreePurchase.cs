using Zenject;

namespace Confrontation
{
	public class FreePurchase : IPurchase
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;

		public bool BuyBuilding(Player player, Building buildingPrefab, Cell locationCell)
		{
			_buildingSpawner.Build(buildingPrefab, locationCell);
			return true;
		}

		public bool UpgradeBuilding(Player player, Building building)
		{
			building.LevelUp();
			return true;
		}
	}
}
namespace Confrontation
{
	public interface IPurchase
	{
		bool BuyBuilding(Player player, Building buildingPrefab, Cell locationCell);
		bool UpgradeBuilding(Player player, Building building);
	}
}
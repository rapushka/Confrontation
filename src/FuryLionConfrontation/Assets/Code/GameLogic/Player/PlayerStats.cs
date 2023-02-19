namespace Confrontation
{
	public class PlayerStats
	{
		public int GoldCount { get; set; }

		public bool IsEnoughGoldFor(int purchasePrice) => GoldCount >= purchasePrice;

		public void Spend(int gold)
		{
			GoldCount -= gold;
		}
	}
}
namespace Confrontation
{
	public interface IBalanceEntry
	{
		IGoldenMine GoldenMineEntry { get; }

		public interface IGoldenMine
		{
			int ProduceCollDownDuration { get; }

			int ProduceAmount { get; }
		}
	}
}
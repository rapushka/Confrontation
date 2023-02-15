namespace Confrontation
{
	public interface IBalanceEntry
	{
		IGoldenMine GoldenMineEntry { get; }

		public interface IGoldenMine
		{
			float ProduceCollDownDuration { get; }

			int ProduceAmount { get; }
		}
	}
}
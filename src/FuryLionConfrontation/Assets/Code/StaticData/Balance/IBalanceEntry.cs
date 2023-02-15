namespace Confrontation
{
	public interface IBalanceEntry
	{
		IGoldenMine GoldenMineEntry { get; }

		public interface IGoldenMine
		{
			int ProduceSpeed { get; }

			int ProduceAmount { get; }
		}

	}
}
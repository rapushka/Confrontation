namespace Confrontation
{
	public class BalanceTable
	{
		private readonly IBalanceEntry[] _entries = new IBalanceEntry[5];

		public IBalanceEntry GetEntryForLevel(int level) => _entries[level];
	}
}
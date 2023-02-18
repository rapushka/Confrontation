namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitBalanceData Unit { get; }

		LeveledList<GoldenMineBalanceData> GoldenMine { get; }

		LeveledList<BarrackBalanceData> Barrack { get; }

		LeveledList<VillageBalanceData> Village { get; }
	}
}
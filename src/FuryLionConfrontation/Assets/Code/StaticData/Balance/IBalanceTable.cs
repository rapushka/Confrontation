namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats Unit { get; }

		LeveledList<GoldenMineStats> GoldenMine { get; }

		LeveledList<BarrackStats> Barrack { get; }

		LeveledList<VillageStats> Village { get; }
	}
}
namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats Unit { get; }

		GoldenMineStats GoldenMine { get; }

		LeveledList<BarrackStats> Barrack { get; }

		LeveledList<VillageStats> Village { get; }
	}
}
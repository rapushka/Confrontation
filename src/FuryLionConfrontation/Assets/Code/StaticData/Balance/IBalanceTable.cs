namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats UnitStats { get; }

		GoldenMineStats GoldenMineStats { get; }

		BarrackStats BarrackStats { get; }

		SettlementStats SettlementStats { get; }

		EnemiesStats EnemiesStats { get; }

		TimeStats TimeStats { get; }

		int BuildPriceFor(Building building);
	}
}
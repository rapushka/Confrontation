namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats UnitStats { get; }

		GoldenMineStats GoldenMineStats { get; }

		BarrackStats BarrackStats { get; }

		SettlementStats SettlementStats { get; }

		ForgeStats ForgeStats { get; }

		EnemiesStats EnemiesStats { get; }

		TimeStats TimeStats { get; }

		FarmStats FarmStats { get; }

		StableStats StableStats { get; }

		int BuildPriceFor(Building building);
	}
}
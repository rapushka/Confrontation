namespace Confrontation
{
	public interface IStatsTable
	{
		UnitStats UnitStats { get; }

		GoldenMineStats GoldenMineStats { get; }

		TowerOfMagesStats TowerOfMagesStats { get; }

		BarrackStats BarrackStats { get; }

		SettlementStats SettlementStats { get; }

		ForgeStats ForgeStats { get; }

		FortStats FortStats { get; }

		QuarryStats QuarryStats { get; }

		WorkshopStats WorkshopStats { get; }

		EnemiesStats EnemiesStats { get; }

		TimeStats TimeStats { get; }

		FarmStats FarmStats { get; }

		StableStats StableStats { get; }

		UserProgressionStats UserProgressionStats { get; }

		int BuildPriceFor(Building building);
	}
}
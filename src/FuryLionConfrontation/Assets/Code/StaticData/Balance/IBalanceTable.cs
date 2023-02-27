namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats UnitStats { get; }

		GoldenMineStats GoldenMineStats { get; }

		BarrackStats BarrackStats { get; }

		VillageStats VillageStats { get; }

		EnemiesStats EnemiesStats { get; }

		int BuildPriceFor(Building building);
	}
}
namespace Confrontation
{
	public interface IBalanceTable
	{
		UnitStats Unit { get; }

		GoldenMineStats GoldenMine { get; }

		BarrackStats Barrack { get; }

		VillageStats Village { get; }
	}
}
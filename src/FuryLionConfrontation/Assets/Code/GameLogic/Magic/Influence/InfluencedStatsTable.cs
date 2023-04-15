using Zenject;

namespace Confrontation
{
	public class InfluencedStatsTable : IStatsTable
	{
		[Inject] private IStatsTable _decoratee;

		public UnitStats UnitStats => _decoratee.UnitStats;

		public GoldenMineStats GoldenMineStats => _decoratee.GoldenMineStats;

		public TowerOfMagesStats TowerOfMagesStats => _decoratee.TowerOfMagesStats;

		public BarrackStats BarrackStats => _decoratee.BarrackStats;

		public SettlementStats SettlementStats => _decoratee.SettlementStats;

		public ForgeStats ForgeStats => _decoratee.ForgeStats;

		public FortStats FortStats => _decoratee.FortStats;

		public QuarryStats QuarryStats => _decoratee.QuarryStats;

		public WorkshopStats WorkshopStats => _decoratee.WorkshopStats;

		public EnemiesStats EnemiesStats => _decoratee.EnemiesStats;

		public TimeStats TimeStats => _decoratee.TimeStats;

		public FarmStats FarmStats => _decoratee.FarmStats;

		public StableStats StableStats => _decoratee.StableStats;

		public int BuildPriceFor(Building building) => _decoratee.BuildPriceFor(building);
	}
}
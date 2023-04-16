using Zenject;

namespace Confrontation
{
	public abstract class StatsTableDecoratorBase : IStatsTable
	{
		[Inject] private IStatsTable _decoratee;

		public virtual UnitStats UnitStats => _decoratee.UnitStats;

		public virtual GoldenMineStats GoldenMineStats => _decoratee.GoldenMineStats;

		public virtual TowerOfMagesStats TowerOfMagesStats => _decoratee.TowerOfMagesStats;

		public virtual BarrackStats BarrackStats => _decoratee.BarrackStats;

		public virtual SettlementStats SettlementStats => _decoratee.SettlementStats;

		public virtual ForgeStats ForgeStats => _decoratee.ForgeStats;

		public virtual FortStats FortStats => _decoratee.FortStats;

		public virtual QuarryStats QuarryStats => _decoratee.QuarryStats;

		public virtual WorkshopStats WorkshopStats => _decoratee.WorkshopStats;

		public virtual EnemiesStats EnemiesStats => _decoratee.EnemiesStats;

		public virtual TimeStats TimeStats => _decoratee.TimeStats;

		public virtual FarmStats FarmStats => _decoratee.FarmStats;

		public virtual StableStats StableStats => _decoratee.StableStats;

		public virtual int BuildPriceFor(Building building) => _decoratee.BuildPriceFor(building);
	}
}
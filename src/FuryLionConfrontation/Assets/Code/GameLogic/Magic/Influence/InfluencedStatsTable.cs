using Zenject;

namespace Confrontation
{
	public class InfluencedStatsTable : StatsTableDecoratorBase, IInitializable
	{
		private UnitStats _unitStats;

		public void Initialize()
		{
			_unitStats = base.UnitStats.Clone().With((us) => us.BaseSpeed *= 2f);
		}

		public override UnitStats UnitStats => _unitStats;
	}
}
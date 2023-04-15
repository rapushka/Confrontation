using System.Collections.Generic;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluencedStatsTable : StatsTableDecoratorBase, IInitializable
	{
		[Inject] private readonly IEnumerable<TargetedInfluence> _influences;

		private UnitStats _unitStats;

		public void Initialize()
		{
			_unitStats = base.UnitStats.Clone();
		}

		public override UnitStats UnitStats => _unitStats
			.With((us) => us.BaseSpeed = InfluenceUnitSpeed(us));

		private float InfluenceUnitSpeed(IUnitStats stats) 
			=> _influences.WithTarget(UnitsSpeed).Influence(stats.BaseSpeed);
	}
}
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Farm : Building
	{
		[Inject] private readonly InfluenceMediator _influence;

		public override string Name => nameof(Farm);

		public override int UpgradePrice => Stats.UpgradePrice;

		public FarmLevelStats CurrentLevelStats
		{
			get
			{
				var currentLevelStats = Stats[Level];
				return new FarmStatsDecorator(currentLevelStats, _influence, this);
			}
		}

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<FarmLevelStats> Stats => StatsTable.FarmStats.LeveledStats;
	}

	public class FarmStatsDecorator : FarmLevelStats
	{
		private readonly InfluenceMediator _influence;
		private readonly Farm _farm;
		private readonly FarmLevelStats _decoratee;

		public FarmStatsDecorator(FarmLevelStats decoratee, InfluenceMediator influence, Farm farm)
		{
			_decoratee = decoratee;
			_influence = influence;
			_farm = farm;
		}

		public override float SpawnAccelerationCoefficient
		{
			get
			{
				var baseValue = _decoratee.SpawnAccelerationCoefficient;
				return _influence.Influence(baseValue, InfluenceTarget.FarmsBonus, _farm);
			}
		}
	}
}
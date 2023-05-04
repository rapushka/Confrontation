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
}
using Zenject;

namespace Confrontation
{
	public class Forge : Building
	{
		[Inject] private readonly InfluenceMediator _influence;

		public override string Name => nameof(Forge);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<ForgeLevelStats> Stats => StatsTable.ForgeStats.LeveledStats;

		public ForgeLevelStats CurrentLevelStats
		{
			get
			{
				var currentLevelStats = Stats[Level];
				return new ForgeStatsDecorator(currentLevelStats, _influence, this);
			}
		}
	}
}
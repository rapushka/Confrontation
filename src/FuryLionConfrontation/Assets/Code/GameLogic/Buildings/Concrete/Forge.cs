namespace Confrontation
{
	public class Forge : Building
	{
		public override string Name => nameof(Forge);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<ForgeLevelStats> Stats => StatsTable.ForgeStats.LeveledStats;

		public ForgeLevelStats CurrentLevelStats => Stats[Level];
	}
}
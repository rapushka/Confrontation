namespace Confrontation
{
	public class Quarry : Building
	{
		public override string Name => nameof(Quarry);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<QuarryLevelStats> Stats => BalanceTable.QuarryStats.LeveledStats;

		public QuarryLevelStats CurrentLevelStats => Stats[Level];
	}
}
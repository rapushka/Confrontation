namespace Confrontation
{
	public class Farm : Building
	{
		public override string Name => nameof(Farm);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<FarmLevelStats> Stats => BalanceTable.FarmStats.LeveledStats;
	}
}
namespace Confrontation
{
	public class Stable : Building
	{
		public override string Name => nameof(Stable);

		public override int UpgradePrice => Stats.UpgradePrice;

		public StableLevelStats CurrentLevelStats => Stats[Level];

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<StableLevelStats> Stats => BalanceTable.StableStats.LeveledStats;
	}
}
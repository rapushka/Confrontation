namespace Confrontation
{
	public class Fort : IndependentBuilding, IPlaceable
	{
		public override string Name => nameof(Fort);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<FortLevelStats> Stats => BalanceTable.FortStats.LeveledStats;

		public FortLevelStats CurrentLevelStats => Stats[Level];
	}
}
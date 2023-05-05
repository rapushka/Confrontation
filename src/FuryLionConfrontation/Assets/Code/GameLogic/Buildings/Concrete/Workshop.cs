namespace Confrontation
{
	public class Workshop : Building
	{
		public override string Name => nameof(Workshop);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<WorkshopLevelStats> Stats => StatsTable.WorkshopStats.LeveledStats;

		public WorkshopLevelStats CurrentLevelStats => Stats[Level];
	}
}
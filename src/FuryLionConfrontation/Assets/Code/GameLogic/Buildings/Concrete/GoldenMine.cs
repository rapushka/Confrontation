namespace Confrontation
{
	public class GoldenMine : ResourcesGenerator
	{
		public override string Name => nameof(GoldenMine);

		public override int UpgradePrice => Stats.UpgradePrice;

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		protected override int MaxLevel => Stats.MaxLevel;

		private int ProducingRate => CurrentLevelStats.Amount;

		private LeveledStats<GeneratorStatsBase> Stats => BalanceTable.GoldenMineStats.LeveledStats;

		private GeneratorStatsBase CurrentLevelStats => Stats[Level];

		protected override void Produce() => OwnerPlayer?.Resources.Gold.Earn(ProducingRate);
	}
}
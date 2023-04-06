namespace Confrontation
{
	public class TowerOfMages : ResourcesGenerator
	{
		public override string Name => nameof(TowerOfMages);

		public override int UpgradePrice => Stats.UpgradePrice;

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		protected override int MaxLevel => Stats.MaxLevel;

		private int ProducingRate => CurrentLevelStats.Amount;

		private LeveledStats<GeneratorStatsBase> Stats => BalanceTable.TowerOfMagesStats.LeveledStats;

		private GeneratorStatsBase CurrentLevelStats => Stats[Level];

		protected override void Produce() => OwnerPlayer?.Resources.Mana.Earn(ProducingRate);
	}
}
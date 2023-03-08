using Zenject;

namespace Confrontation
{
	public class GoldenMine : Generator
	{
		[Inject] private readonly GameSession _gameSession;

		public override float PassedDuration { get; set; }

		public override float CoolDownDuration => CurrentLevelStats.CoolDown;

		public override string Name => nameof(GoldenMine);

		public override int UpgradePrice => Stats.UpgradePrice;

		protected override int MaxLevel => Stats.MaxLevel;

		private LeveledStats<GeneratorStatsBase> Stats => BalanceTable.GoldenMineStats.LeveledStats;

		private int GoldProducingRate => CurrentLevelStats.Amount;

		private Player OwnerPlayer => _gameSession.GetPlayerFor(OwnerPlayerId);

		private GeneratorStatsBase CurrentLevelStats => BalanceTable.GoldenMineStats.LeveledStats[Level];

		public override void Action() => ProduceGold();

		private void ProduceGold() => OwnerPlayer?.Stats.Earn(GoldProducingRate);
	}
}
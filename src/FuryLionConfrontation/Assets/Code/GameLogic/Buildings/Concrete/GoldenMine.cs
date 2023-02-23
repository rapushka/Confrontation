using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IActorWithCoolDown
	{
		[Inject] private readonly GameSession _gameSession;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => CurrentLevelStats.CoolDown;

		public override string Name => nameof(GoldenMine);

		protected override int MaxLevel => BalanceTable.GoldenMineStats.LeveledStats.MaxLevel;

		private int GoldProducingRate => CurrentLevelStats.Amount;

		private Player OwnerPlayer => _gameSession.GetPlayerFor(Field.Regions[Coordinates].OwnerPlayerId);

		private GeneratorStatsBase CurrentLevelStats => BalanceTable.GoldenMineStats.LeveledStats[Level];

		public void Action() => ProduceGold();

		private void ProduceGold() => OwnerPlayer?.Stats.Earn(GoldProducingRate);
	}
}
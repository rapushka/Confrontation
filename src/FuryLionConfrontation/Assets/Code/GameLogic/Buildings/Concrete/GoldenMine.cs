using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IActorWithCoolDown
	{
		[Inject] private readonly GameSession _gameSession;
		[Inject] private readonly GameplayUiMediator _ui;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => Balance.GenerationCoolDown;

		public override string Name => nameof(GoldenMine);

		protected override int MaxLevel => BalanceTable.GoldenMines.MaxLevel;

		private int GoldProducingRate => Balance.GenerationAmount;

		private Player OwnerPlayer => _gameSession.GetPlayerById(Field.Regions[Coordinates].OwnerPlayerId);

		private GoldenMineBalanceData Balance => BalanceTable.GoldenMines[Level];

		public void Action() => ProduceGold();

		private void ProduceGold()
		{
			OwnerPlayer.Stats.GoldCount += GoldProducingRate;
			_ui.UpdateHud();
		}
	}
}
using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IActorWithCoolDown
	{
		[Inject] private readonly GameSession _gameSession;
		[Inject] private readonly GameplayUiMediator _ui;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => Balance.ProduceCollDownDuration;

		private int GoldProducingRate => Balance.ProduceAmount;

		private Player OwnerPlayer => _gameSession.GetPlayerById(Field.Regions[Coordinates].OwnerPlayerId);

		private IGoldenMine Balance => BalanceTable.GoldenMines[Level];

		public void Action() => ProduceGold();

		private void ProduceGold()
		{
			OwnerPlayer.Stats.GoldCount += GoldProducingRate;
			_ui.UpdateHud();
		}
	}
}
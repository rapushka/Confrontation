using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IActorWithCoolDown
	{
		[Inject] private readonly GameplayLoop _gameplayLoop;
		[Inject] private readonly GameplayUiMediator _ui;

		[SerializeField] private int _goldProducingRate = 1;

		[field: SerializeField] public float CoolDownDuration { get; private set; } = 1f;

		private Player _ownerPlayer;

		public float PassedDuration { get; set; }

		public void Action() => ProduceGold();

		public override void Initialize()
		{
			var ownerPlayerId = Field.Regions[Coordinates].OwnerPlayerId;
			_ownerPlayer = _gameplayLoop.GetPlayerById(ownerPlayerId);
		}

		private void ProduceGold()
		{
			_ownerPlayer.Stats.GoldCount += _goldProducingRate;
			_ui.UpdateHud();		
		}
	}
}
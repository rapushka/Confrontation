using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IActorWithCoolDown
	{
		[Inject] private readonly GameplayLoop _gameplayLoop;
		[Inject] private readonly Hud _hud;
		[Inject] private readonly User _user;

		[SerializeField] private int _goldProducingRate = 1;

		[field: SerializeField] public float CoolDownDuration { get; private set; } = 1f;

		private Player _ownerPlayer;

		public float PassedDuration { get; set; }

		public void Action() => ProduceGold();

		public override void Initialize()
		{
			var ownerPlayerId = Field.Regions[Coordinates].OwnerPlayerId;
			_ownerPlayer = _gameplayLoop.GetPlayerWithId(ownerPlayerId);
		}

		private void ProduceGold()
		{
			_ownerPlayer.Stats.GoldCount += _goldProducingRate;
			if (_ownerPlayer.Id == _user.Player.Id)
			{
				_hud.GoldenAmount = _ownerPlayer.Stats.GoldCount;
			}
		}
	}
}
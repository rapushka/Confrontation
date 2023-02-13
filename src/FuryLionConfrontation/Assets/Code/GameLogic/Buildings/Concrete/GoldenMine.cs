using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GoldenMine : Building, IInitializable, IActorWithCoolDown
	{
		[Inject] private readonly GameplayLoop _gameplayLoop;

		[SerializeField] private int _goldProducingRate = 1;

		[field: SerializeField] public float CoolDownDuration { get; private set; } = 1f;

		private Player _ownerPlayer;

		public float PassedDuration { get; set; }

		public void Action() => ProduceGold();

		public void Initialize()
		{
			var ownerPlayerId = Field.Regions[Coordinates].OwnerPlayerId;
			_ownerPlayer = _gameplayLoop.GetPlayerWithId(ownerPlayerId);
		}

		private void ProduceGold()
		{
			_ownerPlayer.Stats.GoldCount += _goldProducingRate;
		}
	}
}
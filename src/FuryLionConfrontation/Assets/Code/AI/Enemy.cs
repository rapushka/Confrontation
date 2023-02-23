using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Enemy : IActorWithCoolDown
	{
		[Inject] private readonly Player _player;
		[Inject] private readonly IBalanceTable _balance;

		public float PassedDuration { get; set; }

		public float CoolDownDuration => _balance.EnemiesStats.SecondsBetweenActions;

		public void Action()
		{
			Debug.Log($"Enemy {_player.Id} doing something!");
		}

		public class Factory : PlaceholderFactory<Player, Enemy> { }
	}
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayLoop
	{
		[Inject] private readonly User _user;

		private readonly HashSet<Player> _activePlayers = new();

		public void AddPlayer(Player player) => _activePlayers.Add(player);

		public void PlayerLoose(int id)
		{
			_activePlayers.RemoveById(id);

			if (_activePlayers.Count == 1)
			{
				GameEnd(_activePlayers.Single());
			}
		}

		private void GameEnd(Player winner) => Debug.Log(winner.Equals(_user.Player) ? "Victory" : "You Lose");
	}
}
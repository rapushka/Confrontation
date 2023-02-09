using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public class GameplayLoop
	{
		private readonly HashSet<Player> _players = new();

		public void AddPlayer(Player player) => _players.Add(player);

		public void PlayerLoose(int id)
		{
			Debug.Assert(_players.GetWithId(id).IsLost == false);

			_players.GetWithId(id).IsLost = true;
		}
	}
}
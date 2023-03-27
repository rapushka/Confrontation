using System.Collections.Generic;

namespace Confrontation
{
	public class LevelEditSession : ISession
	{
		private readonly HashSet<Player> _players = new();

		public void AddPlayer(Player player) => _players.Add(player);

		public Player GetPlayerFor(int id) => _players.GetPlayerByIdOrDefault(id);

		public void PlayerLoose(int id) => _players.RemoveById(id);
	}
}
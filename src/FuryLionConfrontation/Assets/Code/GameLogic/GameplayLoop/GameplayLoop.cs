using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class GameplayLoop : IInitializable
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		private readonly HashSet<Player> _activePlayers = new();

		public void AddPlayer(Player player) => _activePlayers.Add(player);

		public Player GetPlayerById(int id) => _activePlayers.GetPlayerById(id);

		public void Initialize() => _user.InitializePlayer(GetPlayerById);

		public void PlayerLoose(int id)
		{
			_activePlayers.RemoveById(id);

			if (_activePlayers.Count == 1)
			{
				GameEnd(_activePlayers.Single());
			}
		}

		private void GameEnd(Player winner)
		{
			_user.GameResult = winner.Equals(_user.Player) ? GameResult.Victory : GameResult.Loose;
			_uiMediator.OpenWindow<GameResultsWindow>();
		}
	}
}
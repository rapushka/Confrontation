using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class GameSession : IInitializable, ISession
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		private readonly HashSet<Player> _activePlayers = new();

		public event Action<int> EnemyLoose;

		public IEnumerable<Player> Enemies => _activePlayers.Where((p) => p.Id != _user.PlayerId);

		private bool IsAllEnemiesDefeat => _activePlayers.Count == 1;

		public void AddPlayer(Player player) => _activePlayers.Add(player);

		public Player GetPlayerFor(int id) => _activePlayers.GetPlayerByIdOrDefault(id);

		public void Initialize() => _user.Construct(GetPlayerFor(_user.PlayerId));

		public void PlayerLoose(int looserId)
		{
			_activePlayers.RemoveById(looserId);

			if (looserId == _user.PlayerId)
			{
				GameLost();
				return;
			}

			EnemyLoose?.Invoke(looserId);

			if (IsAllEnemiesDefeat)
			{
				GameWin();
			}
		}

		private void GameWin() => GameEnd(GameResult.Victory);

		private void GameLost() => GameEnd(GameResult.Loose);

		private void GameEnd(GameResult gameResult)
		{
			_user.GameResult = gameResult;
			_uiMediator.OpenWindow<GameResultsWindow>();
		}
	}
}
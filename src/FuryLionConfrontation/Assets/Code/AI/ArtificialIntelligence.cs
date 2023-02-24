using System;
using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class ArtificialIntelligence : IInitializable, IDisposable
	{
		[Inject] private readonly GameSession _gameSession;
		[Inject] private readonly Enemy.Factory _enemyFactory;

		private readonly Dictionary<int, Enemy> _enemies = new();

		public IEnumerable<Enemy> Enemies => _enemies.Values;

		public void Initialize()
		{
			_gameSession.EnemyLoose += OnEnemyLoose;

			foreach (var enemyPlayer in _gameSession.Enemies)
			{
				_enemies.Add(enemyPlayer.Id, _enemyFactory.Create(enemyPlayer));
			}
		}

		public void Dispose() => _gameSession.EnemyLoose -= OnEnemyLoose;

		private void OnEnemyLoose(int id)
		{
			var enemy = _enemies[id];
			enemy.Loose();
			_enemies.Remove(id);
		}
	}
}
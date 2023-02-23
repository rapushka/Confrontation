using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class ArtificialIntelligence : IInitializable
	{
		[Inject] private readonly GameSession _gameSession;
		[Inject] private readonly Enemy.Factory _enemyFactory;

		private readonly List<Enemy> _enemies = new();

		public IEnumerable<Enemy> Enemies => _enemies;

		public void Initialize()
		{
			foreach (var enemyPlayer in _gameSession.Enemies)
			{
				_enemies.Add(_enemyFactory.Create(enemyPlayer));
			}
		}
	}
}
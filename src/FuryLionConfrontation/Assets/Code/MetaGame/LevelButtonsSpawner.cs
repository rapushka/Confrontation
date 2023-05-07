using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelButtonsSpawner : IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;
		[Inject] private readonly LevelButtonBase.Factory _levelButtonsFactory;
		[Inject] private readonly Transform _levelsGridRoot;

		private int _counter = 1;

		protected virtual Transform Parent => _levelsGridRoot;

		public void Initialize() => _levels.ForEach(level => Create(level));

		protected virtual LevelButtonBase Create(ILevel level)
		{
			var levelButtonBase = _levelButtonsFactory.Create<LevelButtonBase>(_counter, level);
			_counter++;
			return levelButtonBase;
		}
	}
}
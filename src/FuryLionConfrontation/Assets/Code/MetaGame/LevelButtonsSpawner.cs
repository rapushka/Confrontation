using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class LevelButtonsSpawner : IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;
		[Inject] private readonly LevelButtonBase.Factory _levelButtonsFactory;

		private int _counter = 1;

		public void Initialize() => _levels.ForEach(level => Create(level));

		protected virtual LevelButtonBase Create(ILevel level)
		{
			var levelButtonBase = _levelButtonsFactory.Create<LevelButtonBase>(_counter, level);
			_counter++;
			return levelButtonBase;
		}
	}
}
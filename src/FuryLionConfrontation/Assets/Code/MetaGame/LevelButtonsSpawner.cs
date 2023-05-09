using System.Collections.Generic;
using Zenject;

namespace Confrontation
{
	public class LevelButtonsSpawner : IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;
		[Inject] private readonly LevelButtonBase.Factory _levelButtonsFactory;
		[Inject] private readonly IProgressionStorageService _progression;

		private int _counter = 1;

		private bool IsUnlocked => UnlockedLevelsCount >= _counter;

		private int UnlockedLevelsCount => _progression.LoadProgress().CompletedLevelsCount + 1;

		public void Initialize() => _levels.ForEach(level => Create(level));

		protected virtual LevelButtonBase Create(ILevel level)
		{
			var levelButtonBase = _levelButtonsFactory.Create<LevelButtonBase>(_counter, level, IsUnlocked);
			_counter++;
			return levelButtonBase;
		}
	}
}
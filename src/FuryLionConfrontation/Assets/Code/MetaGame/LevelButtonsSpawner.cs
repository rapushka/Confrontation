using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelButtonsSpawner : IInitializable
	{
		[Inject] private readonly List<LevelScriptableObject> _levels;
		[Inject] private readonly LevelButton.Factory _levelButtonsFactory;
		[Inject] private readonly Transform _levelsGridRoot;

		private int _counter = 1;

		public void Initialize() => _levels.ForEach(Create);

		private void Create(ILevel level)
		{
			var levelButton = _levelButtonsFactory.Create(_counter, level);
			levelButton.transform.SetParent(_levelsGridRoot);
			_counter++;
		}
	}
}
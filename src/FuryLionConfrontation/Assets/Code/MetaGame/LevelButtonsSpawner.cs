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

		public void Initialize() => _levels.ForEach(Create);

		private void Create(ILevel level)
		{
			var levelButton = _levelButtonsFactory.Create<LevelButtonBase>(_counter, level);
			// levelButton.transform.SetParent(Parent);
			_counter++;
		}
	}
}
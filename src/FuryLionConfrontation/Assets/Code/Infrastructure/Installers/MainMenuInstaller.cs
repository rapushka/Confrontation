using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private LevelButton _levelButtonPrefab;
		[SerializeField] private List<LevelScriptableObject> _levels;
		[SerializeField] private Transform _levelsGridRoot;

		public override void InstallBindings()
		{
			Container.BindInstance(_levelButtonPrefab).AsSingle();
			
			InstallForLevelButtonsSpawner();

			Container.Bind<ToGameplay>().AsSingle();
			Container.BindInterfacesTo<LevelButtonsSpawner>().AsSingle();

			Container.BindFactory<int, ILevel, LevelButton, LevelButton.Factory>()
			         .FromComponentInNewPrefab(_levelButtonPrefab);
		}

		private void InstallForLevelButtonsSpawner()
		{
			Container.BindInstance(_levelsGridRoot).WhenInjectedInto<LevelButtonsSpawner>();
			Container.BindInstance(_levels).WhenInjectedInto<LevelButtonsSpawner>();
		}
	}
}
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class MainMenuInstaller : MonoInstaller
	{
		[SerializeField] private LevelButton _levelButtonPrefab;

		public override void InstallBindings()
		{
			Container.BindInstance(_levelButtonPrefab).AsSingle();

			Container.Bind<ToGameplay>().AsSingle();

			Container.BindFactory<int, Level, LevelButton, LevelButton.Factory>()
			         .FromComponentInNewPrefab(_levelButtonPrefab);
		}
	}
}
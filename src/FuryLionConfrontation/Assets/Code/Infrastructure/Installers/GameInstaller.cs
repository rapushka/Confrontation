using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;
		[SerializeField] private ResourcesService _resources;
		[SerializeField] private RectTransform _canvasPrefab;
		[SerializeField] private List<WindowBase> _windows;
		[SerializeField] private InputService _inputService;
		[SerializeField] private LevelScriptableObject _level;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			BindPrefabs();

			Container.Bind<ILevel>().FromInstance(_level).AsSingle();
			Container.Bind<ILevelSelector>().To<InjectedLevelSelector>().AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<TimeService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<GameUiMediator>().AsSingle();

			StartGame();
		}

		private void StartGame() => Container.BindInterfacesTo<Bootstrapper>().AsSingle();

		private void BindPrefabs()
		{
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
			Container.BindInstance(_canvasPrefab).AsSingle();
			Container.BindInstance(new TypedDictionary<WindowBase>(_windows)).AsSingle();
			Container.Bind<IInputService>().FromComponentInNewPrefab(_inputService).AsSingle();
		}
	}
}
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
		[SerializeField] private BuildingButton _buildingButton;
		[SerializeField] private InputService _inputService;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			BindPrefabs();

			Container.Bind<ILevelSelector>().To<LevelCreator>().AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<Windows>().AsSingle();
			Container.Bind<UiMediator>().AsSingle();
			Container.Bind<BuildingSpawner>().AsSingle();

			Container.BindInterfacesTo<ToBootstrap>().AsSingle();

			BindFactories();
		}

		private void BindPrefabs()
		{
			Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
			Container.BindInstance(_user).AsSingle();
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
			Container.BindInstance(_canvasPrefab).AsSingle();
			Container.BindInstance(new TypedDictionary<WindowBase>(_windows)).AsSingle();
			Container.Bind<IInputService>().FromComponentInNewPrefab(_inputService).AsSingle();
		}

		private void BindFactories()
		{
			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingWindow, BuildingWindow.Factory>();
			Container.BindFactory<WindowBase, WindowBase, WindowBase.Factory>().FromFactory<CustomWindowFactory>();

			Container.BindFactory<Building, Building, Building.Factory>()
			         .FromFactory<PrefabFactory<Building>>();

			Container.BindFactory<Building, BuildingButton, BuildingButton.Factory>()
			         .FromComponentInNewPrefab(_buildingButton);
		}
	}
}
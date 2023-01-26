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

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInstance<IResourcesService>(_resources).AsSingle();
			Container.BindInterfacesAndSelfTo<User>().FromInstance(_user).AsSingle();
			Container.BindInstance(_canvasPrefab).AsSingle();
			Container.BindInstance(Instantiate(_loadingCurtainPrefab)).AsSingle();
			Container.BindInstance(new TypedDictionary<WindowBase>(_windows)).AsSingle();

			Container.BindInterfacesTo<AssetsService>().AsSingle();
			Container.BindInterfacesTo<SceneTransferService>().AsSingle();

			Container.Bind<Windows>().AsSingle();
			Container.Bind<UiMediator>().AsSingle();

			Container.BindInterfacesTo<ToBootstrap>().AsSingle();

			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingWindow, BuildingWindow.Factory>();
			Container.BindPrefabFactory<WindowBase, WindowBase.Factory, CustomWindowFactory>();
		}
	}
}
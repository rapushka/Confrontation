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
		[SerializeField] private List<WindowBase> _windows;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			var loadingCurtain = Instantiate(_loadingCurtainPrefab);
			var typedDictionary = new TypedDictionary<WindowBase>(_windows);

			Container.Bind<IResourcesService>().FromInstance(_resources).AsSingle();
			Container.Bind<User>().FromInstance(_user).AsSingle();
			Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();
			Container.Bind<TypedDictionary<WindowBase>>().FromInstance(typedDictionary).AsSingle();

			Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			Container.Bind<ISceneTransferService>().To<SceneTransferService>().AsSingle();

			Container.Bind<Windows>().AsSingle();
			Container.Bind<UiMediator>().AsSingle();

			Container.BindInterfacesTo<ToBootstrap>().AsSingle();

			Container
				.BindFactory<Object, BuildWindow, BuildWindow.Factory>()
				.FromFactory<PrefabFactory<BuildWindow>>();
			Container
				.BindFactory<Object, BuildingWindow, BuildingWindow.Factory>()
				.FromFactory<PrefabFactory<BuildingWindow>>();
			Container
				.BindFactory<Object, WindowBase, WindowBase.FactoryBase>()
				.FromFactory<CustomWindowFactory>();
		}
	}
}
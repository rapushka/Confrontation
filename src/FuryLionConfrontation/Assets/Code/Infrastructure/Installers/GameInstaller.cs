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

			BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			BindPrefabFactory<BuildingWindow, BuildingWindow.Factory>();

			BindPrefabFactory<WindowBase, WindowBase.FactoryBase, CustomWindowFactory>();
		}

		private void BindPrefabFactory<TContract, TFactory>()
			where TFactory : PlaceholderFactory<Object, TContract>
			=> BindPrefabFactory<TContract, TFactory, PrefabFactory<TContract>>();

		private void BindPrefabFactory<TContract, TFactory, TActualFactory>()
			where TFactory : PlaceholderFactory<Object, TContract>
			where TActualFactory : IFactory<Object, TContract>
		{
			Container
				.BindFactory<Object, TContract, TFactory>()
				.FromFactory<TActualFactory>();
		}
	}
}
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;
		[SerializeField] private ResourcesService _resources;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			var loadingCurtain = Instantiate(_loadingCurtainPrefab);

			Container.Bind<IResourcesService>().FromInstance(_resources).AsSingle();
			Container.Bind<User>().FromInstance(_user).AsSingle();
			Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();

			Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			Container.Bind<ISceneTransferService>().To<SceneTransferService>().AsSingle();

			Container.Bind<Windows>().AsSingle();
			Container.Bind<UiMediator>().AsSingle();

			Container.BindInterfacesTo<ToBootstrap>().AsSingle();
		}
	}
}
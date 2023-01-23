using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;
		[SerializeField] private User _user;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			var loadingCurtain = Instantiate(_loadingCurtainPrefab);

			Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();
			Container.Bind<User>().FromInstance(_user).AsSingle();
			Container.Bind<UiMediator>().AsSingle();
			Container.Bind<ISceneTransferService>().To<SceneTransferService>().AsSingle();
			Container.BindInterfacesTo<ToBootstrap>().AsSingle();
		}
	}
}
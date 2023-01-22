using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LoadingCurtain _loadingCurtainPrefab;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.Bind<ISceneTransferService>().To<SceneTransferService>().AsSingle();
			Container.BindInterfacesTo<ToBootstrap>().AsSingle();
			Container.BindInterfacesTo<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtainPrefab).AsSingle();
		}
	}
}
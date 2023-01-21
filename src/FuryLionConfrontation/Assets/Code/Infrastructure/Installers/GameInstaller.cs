using Zenject;

namespace Confrontation
{
	public class GameInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.Bind<ISceneTransferService>().To<SceneTransferService>().AsSingle();
		}
	}
}
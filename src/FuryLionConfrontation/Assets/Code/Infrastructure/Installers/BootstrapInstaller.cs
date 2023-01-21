using Zenject;

namespace Confrontation
{
	public class BootstrapInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<GameBootstrapper>().AsSingle();
		}
	}
}
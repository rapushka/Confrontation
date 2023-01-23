using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller
	{
		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<Field>().AsSingle();
			Container.BindInterfacesAndSelfTo<Regions>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldClicksHandler>().AsSingle();
		}
	}
}
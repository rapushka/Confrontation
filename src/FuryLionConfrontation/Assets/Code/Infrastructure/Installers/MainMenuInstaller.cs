using Zenject;

namespace Confrontation
{
	public class MainMenuInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ToMainMenuOnInitialize>().AsSingle();
		}
	}
}
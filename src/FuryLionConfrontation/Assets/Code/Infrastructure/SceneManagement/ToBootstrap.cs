using Zenject;

namespace Confrontation
{
	public class ToBootstrap : ToScene
	{
		[Inject] public ToBootstrap(ISceneTransferService sceneTransfer) : base(sceneTransfer) { }

		protected override string SceneName => Constants.SceneName.BootstrapScene;
	}
}
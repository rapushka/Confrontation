using Zenject;

namespace Confrontation
{
	public class ToBootstrap : IInitializable
	{
		private readonly ISceneTransferService _sceneTransfer;

		[Inject] public ToBootstrap(ISceneTransferService sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => _sceneTransfer.ToScene(Constants.SceneName.BootstrapScene);
	}
}
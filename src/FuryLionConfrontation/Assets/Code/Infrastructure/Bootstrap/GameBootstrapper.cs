using Zenject;

namespace Confrontation
{
	public class GameBootstrapper : IInitializable
	{
		private readonly ISceneTransferService _sceneTransfer;

		[Inject] public GameBootstrapper(ISceneTransferService sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => _sceneTransfer.ToScene(Constants.SceneName.BootstrapScene);
	}
}
using Zenject;

namespace Confrontation
{
	public class ToGameplay : IInitializable
	{
		private readonly ISceneTransferService _sceneTransfer;

		[Inject] public ToGameplay(ISceneTransferService sceneTransfer) => _sceneTransfer = sceneTransfer;

		public void Initialize() => _sceneTransfer.ToScene(Constants.SceneName.GameplayScene);
	}
}
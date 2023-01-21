using Zenject;

namespace Confrontation
{
	public class ToGameplay : ToScene
	{
		[Inject] public ToGameplay(ISceneTransferService sceneTransfer) : base(sceneTransfer) { }

		protected override string SceneName => Constants.SceneName.GameplayScene;
	}
}
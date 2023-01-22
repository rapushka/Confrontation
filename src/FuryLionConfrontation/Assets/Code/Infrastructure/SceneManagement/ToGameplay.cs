using Zenject;

namespace Confrontation
{
	public class ToGameplay : ToScene
	{
		private readonly LoadingCurtain _loadingCurtain;

		[Inject]
		public ToGameplay(ISceneTransferService sceneTransfer, LoadingCurtain loadingCurtain) :
			base(sceneTransfer)
			=> _loadingCurtain = loadingCurtain;

		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override void Initialize()
		{
			base.Initialize();
			_loadingCurtain.Hide();
		}
	}
}
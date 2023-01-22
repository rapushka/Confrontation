using Zenject;

namespace Confrontation
{
	public class ToBootstrap : ToScene
	{
		private readonly LoadingCurtain _loadingCurtain;

		[Inject]
		public ToBootstrap(ISceneTransferService sceneTransfer, LoadingCurtain loadingCurtain)
			: base(sceneTransfer)
			=> _loadingCurtain = loadingCurtain;

		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override void Initialize()
		{
			_loadingCurtain.ShowImmediately();
			base.Initialize();
		}
	}
}
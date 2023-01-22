using Zenject;

namespace Confrontation
{
	public class ToBootstrap : ToScene
	{
		private readonly SignalBus _signalBus;

		[Inject]
		public ToBootstrap(ISceneTransferService sceneTransfer, SignalBus signalBus)
			: base(sceneTransfer)
			=> _signalBus = signalBus;

		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override void Initialize()
		{
			_signalBus.Fire<LoadingCurtainShowImmediately>();
			base.Initialize();
		}
	}
}
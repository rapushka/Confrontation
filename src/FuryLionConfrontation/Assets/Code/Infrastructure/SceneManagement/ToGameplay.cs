using Zenject;

namespace Confrontation
{
	public class ToGameplay : ToScene
	{
		private readonly SignalBus _signalBus;

		[Inject]
		public ToGameplay(ISceneTransferService sceneTransfer, SignalBus signalBus) :
			base(sceneTransfer)
			=> _signalBus = signalBus;

		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override void Initialize()
		{
			base.Initialize();
			_signalBus.Fire<LoadingCurtainHide>();
		}
	}
}
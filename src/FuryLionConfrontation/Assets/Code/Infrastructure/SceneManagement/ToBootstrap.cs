using Zenject;

namespace Confrontation
{
	public class ToBootstrap : ToScene
	{
		private readonly UiMediator _mediator;

		[Inject]
		public ToBootstrap(ISceneTransferService sceneTransfer, UiMediator mediator)
			: base(sceneTransfer)
			=> _mediator = mediator;

		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override void Initialize()
		{
			_mediator.ShowImmediatelyLoadingCurtain();
			base.Initialize();
		}
	}
}
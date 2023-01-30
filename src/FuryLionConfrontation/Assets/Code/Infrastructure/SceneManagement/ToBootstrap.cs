using Zenject;

namespace Confrontation
{
	public class ToBootstrap : ToScene
	{
		private readonly GameUiMediator _mediator;

		[Inject]
		public ToBootstrap(ISceneTransferService sceneTransfer, GameUiMediator mediator)
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
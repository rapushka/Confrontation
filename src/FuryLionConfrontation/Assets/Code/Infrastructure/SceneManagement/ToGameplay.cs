using Zenject;

namespace Confrontation
{
	public class ToGameplay : ToScene
	{
		private readonly GameUiMediator _mediator;

		[Inject]
		public ToGameplay(ISceneTransferService sceneTransfer, GameUiMediator mediator)
			: base(sceneTransfer)
			=> _mediator = mediator;

		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override void Initialize()
		{
			base.Initialize();
			_mediator.HideLoadingCurtain();
		}
	}
}
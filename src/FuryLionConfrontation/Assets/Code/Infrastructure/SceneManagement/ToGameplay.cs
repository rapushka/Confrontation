using Zenject;

namespace Confrontation
{
	public class ToGameplay : ToScene
	{
		[Inject] private readonly GameUiMediator _mediator;

		protected override string SceneName => Constants.SceneName.GameplayScene;

		public override void Initialize()
		{
			base.Initialize();
			_mediator.HideLoadingCurtain();
		}
	}
}
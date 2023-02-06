using Zenject;

namespace Confrontation
{
	public class Bootstrapper : ToScene
	{
		[Inject] private readonly GameUiMediator _mediator;

		protected override string SceneName => Constants.SceneName.BootstrapScene;

		public override void Initialize()
		{
			_mediator.ShowImmediatelyLoadingCurtain();
			base.Initialize();
		}
	}
}
using Zenject;

namespace Confrontation
{
	public class Tutorial : IInitializable
	{
		[Inject] private readonly GameplayUiMediator _ui;

		public void Initialize()
		{
			_ui.OpenWindow<TutorialWindow>();
		}
	}
}
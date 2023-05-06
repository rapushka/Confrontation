using Zenject;

namespace Confrontation
{
	public class Tutorial : IInitializable
	{
		[Inject] private readonly TimeStopService _timeStopService;
		[Inject] private readonly GameplayUiMediator _ui;

		public void Initialize()
		{
			_timeStopService.Stop();
			_ui.OpenWindow<TutorialWindow>();
		}
	}
}
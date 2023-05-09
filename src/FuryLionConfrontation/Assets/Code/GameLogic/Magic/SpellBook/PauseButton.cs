using Zenject;

namespace Confrontation
{
	public class PauseButton : ButtonBase
	{
		[Inject] private readonly GameplayWindows _gameplayWindows;

		protected override void OnButtonClick() => _gameplayWindows.Open<PauseWindow>();
	}
}
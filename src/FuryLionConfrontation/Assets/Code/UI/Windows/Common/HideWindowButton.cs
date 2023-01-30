using Zenject;

namespace Confrontation
{
	public class HideWindowButton : ButtonBase
	{
		[Inject] private readonly GameUiMediator _gameUiMediator;

		protected override void OnButtonClick() => _gameUiMediator.CloseCurrentWindow();
	}
}
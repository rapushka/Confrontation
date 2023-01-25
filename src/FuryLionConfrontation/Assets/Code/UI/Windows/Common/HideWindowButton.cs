using Zenject;

namespace Confrontation
{
	public class HideWindowButton : ButtonBase
	{
		[Inject] private readonly UiMediator _uiMediator;

		protected override void OnButtonClick() => _uiMediator.HideWindow();
	}
}
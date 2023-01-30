using Zenject;

namespace Confrontation
{
	public class HideWindowButton : ButtonBase
	{
		[Inject] private readonly GameplayUiMediator _uiMediator;

		protected override void OnButtonClick() => _uiMediator.CloseCurrentWindow();
	}
}
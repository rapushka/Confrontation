using Zenject;

namespace Confrontation
{
	public class HideGameplayWindowButton : ButtonBase
	{
		[Inject] private readonly GameplayUiMediator _uiMediator;

		protected override void OnButtonClick() => _uiMediator.CloseCurrentWindow();
	}
}
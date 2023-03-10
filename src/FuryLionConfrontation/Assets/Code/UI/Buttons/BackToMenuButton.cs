using Zenject;

namespace Confrontation
{
	public class BackToMenuButton : ButtonBase
	{
		[Inject] private readonly ToMainMenu _toMainMenu;

		protected override async void OnButtonClick() => await _toMainMenu.Transfer();
	}
}
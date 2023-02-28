using Zenject;

namespace Confrontation
{
	public class BackToMenuButton : ButtonBase
	{
		[Inject] private readonly ToMainMenu _toMainMenu;

		protected override void OnButtonClick() => _toMainMenu.Transfer();
	}
}
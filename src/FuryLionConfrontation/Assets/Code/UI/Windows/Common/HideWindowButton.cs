using Zenject;

namespace Confrontation
{
	public class HideWindowButton : ButtonBase
	{
		[Inject] private readonly WindowsStack<WindowBase> _windows;

		protected override void OnButtonClick() => _windows.Close();
	}
}
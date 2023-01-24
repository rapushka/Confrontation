using UnityEngine;

namespace Confrontation
{
	public class WindowCloseButton : ButtonBase
	{
		[SerializeField] private WindowBase _window;

		protected override void OnButtonClick() => _window.Hide();
	}
}
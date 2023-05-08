using UnityEngine;

namespace Confrontation
{
	public class OpenWindowButton : ButtonBase
	{
		[SerializeField] private WindowBase _window;

		protected override void OnButtonClick() => _window.Open();
	}
}
using UnityEngine;

namespace Confrontation
{
	public class HideWindowButton : ButtonBase
	{
		[SerializeField] private WindowBase _window;

		protected override void OnButtonClick() => _window.Close();
	}
}
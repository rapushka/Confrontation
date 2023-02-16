using System;

namespace Confrontation
{
	public class EventButton : ButtonBase
	{
		public event Action Click;
		
		protected override void OnButtonClick() => Click?.Invoke();
	}
}
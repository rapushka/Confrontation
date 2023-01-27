using System;

namespace Confrontation
{
	public interface IInputService
	{
		event Action<ClickReceiver> Clicked;
	}
}
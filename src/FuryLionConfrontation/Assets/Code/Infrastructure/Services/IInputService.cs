using System;

namespace Confrontation
{
	public interface IInputService
	{
		event Action<ClickReceiver> Clicked;

		event Action<ClickReceiver, ClickReceiver> Dragged;
	}
}
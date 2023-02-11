using System;
using UnityEngine;

namespace Confrontation
{
	public interface IInputService
	{
		event Action<ClickReceiver> Clicked;

		event Action<ClickReceiver> DragStart;

		event Action DragEnd;

		Vector3 CursorWorldPosition { get; }

		Cell ClickedCell { get; set; }

		event Action<ClickReceiver> DragDropped;
	}
}
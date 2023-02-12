using System;
using UnityEngine;

namespace Confrontation
{
	public interface IInputService
	{
		event Action<ClickReceiver> Clicked;

		event Action<ClickReceiver> DragStart;

		event Action<ClickReceiver> DragEnd;

		event Action<Vector3> SwipeStart;

		event Action SwipeEnd;

		Vector3 CursorWorldPosition { get; }

		Cell ClickedCell { get; set; }
	}
}
using System;
using UnityEngine;

namespace Confrontation
{
	public interface IInputService
	{
		event Action<ClickReceiver> Clicked;

		event Action<ClickReceiver, ClickReceiver> Dragged;

		event Action<Vector3> DragStart;

		event Action DragEnd;

		Vector3 CursorWorldPosition { get; }
	}
}
using System;

namespace Confrontation
{
	public class DragAndDrop
	{
		private ClickReceiver _startReceiver;

		public event Action<ClickReceiver, ClickReceiver> Dragged;

		private bool IsDraggingStarted => _startReceiver == true;

		public void StartDragging(ClickReceiver startReceiver)
		{
			_startReceiver = startReceiver;
		}

		public void StopDragging(ClickReceiver endReceiver)
		{
			if (IsDraggingStarted
			    && endReceiver.Equals(_startReceiver) == false)
			{
				Dragged?.Invoke(_startReceiver, endReceiver);
			}

			_startReceiver = null;
		}
	}
}
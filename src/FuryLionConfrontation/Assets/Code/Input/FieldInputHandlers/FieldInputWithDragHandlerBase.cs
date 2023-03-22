namespace Confrontation
{
	public abstract class FieldInputWithDragHandlerBase : FieldInputHandlerBase
	{
		private ClickReceiver _startReceiver;

		private bool IsDraggingStarted => _startReceiver == true;

		public override void Initialize()
		{
			base.Initialize();

			InputService.DragStart += OnDragStarted;
			InputService.DragEnd += OnDragEnd;
		}

		public override void Dispose()
		{
			InputService.DragStart -= OnDragStarted;
			InputService.DragEnd -= OnDragEnd;

			base.Dispose();
		}

		private void OnDragStarted(ClickReceiver startReceiver) => _startReceiver = startReceiver;

		private void OnDragEnd(ClickReceiver endReceiver)
		{
			if (IsDraggingStarted
			    && endReceiver.Equals(_startReceiver) == false)
			{
				OnCellsDrag(_startReceiver, endReceiver);
			}

			_startReceiver = null;
		}

		protected abstract void OnCellsDrag(ClickReceiver startReceiver, ClickReceiver endReceiver);
	}
}
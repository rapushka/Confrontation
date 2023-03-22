using System;
using Zenject;

namespace Confrontation
{
	public abstract class FieldInputHandlerBase : IInitializable, IDisposable
	{
		[Inject] protected readonly IInputService InputService;
		[Inject] protected readonly User User;

		private ClickReceiver _startReceiver;
		private bool IsDraggingStarted => _startReceiver == true;

		public virtual void Initialize()
		{
			InputService.Clicked += OnClick;
			InputService.DragStart += OnDragStarted;
			InputService.DragEnd += OnDragEnd;
		}

		public virtual void Dispose()
		{
			InputService.Clicked -= OnClick;
			InputService.DragStart -= OnDragStarted;
			InputService.DragEnd -= OnDragEnd;
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

		private void OnClick(ClickReceiver clickReceiver) => OnCellClick(clickReceiver.Cell);

		private void OnCellClick(Cell cell)
		{
			InputService.ClickedCell = cell;
			if (cell.IsBelongTo(User.Player))
			{
				ShowRelevantMenu(cell);
			}
		}

		protected abstract void ShowRelevantMenu(Cell cell);
	}
}
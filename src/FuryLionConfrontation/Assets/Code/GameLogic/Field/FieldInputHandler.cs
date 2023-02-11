using System;
using Zenject;

namespace Confrontation
{
	public class FieldInputHandler : IInitializable, IDisposable
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly Orders _orders;

		private ClickReceiver _startReceiver;

		private bool IsDraggingStarted => _startReceiver == true;

		public void Initialize()
		{
			_inputService.Clicked += OnClick;
			_inputService.DragStart += OnDragStarted;
			_inputService.DragDropped += OnDragDropped;
		}

		public void Dispose()
		{
			_inputService.Clicked -= OnClick;
			_inputService.DragStart -= OnDragStarted;
			_inputService.DragDropped -= OnDragDropped;
		}

		private void OnDragStarted(ClickReceiver startReceiver) => _startReceiver = startReceiver;

		private void OnDragDropped(ClickReceiver endReceiver)
		{
			if (IsDraggingStarted
			    && endReceiver.Equals(_startReceiver) == false)
			{
				_orders.GiveOrder(_startReceiver.Cell, endReceiver.Cell);
			}

			_startReceiver = null;
		}

		private void OnClick(ClickReceiver clickReceiver) => OnCellClick(clickReceiver.Cell);

		private void OnCellClick(Cell cell)
		{
			_inputService.ClickedCell = cell;
			if (cell.IsBelongTo(_user.Player))
			{
				ShowRelevantMenu(cell);
			}
		}

		private void ShowRelevantMenu(Cell cell)
		{
			if (cell.IsEmpty)
			{
				_uiMediator.OpenWindow<BuildWindow>();
			}
			else
			{
				_uiMediator.OpenWindow<BuildingWindow>();
			}
		}
	}
}
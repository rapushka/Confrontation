using System;
using Zenject;

namespace Confrontation
{
	public class FieldClicksHandler : IInitializable, IDisposable
	{
		[Inject] private readonly User _user;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly IInputService _inputService;
		[Inject] private readonly Orders _orders;

		public void Initialize()
		{
			_inputService.Clicked += OnClick;
			_inputService.Dragged += OnDrag;
		}

		public void Dispose()
		{
			_inputService.Clicked -= OnClick;
			_inputService.Dragged -= OnDrag;
		}

		private void OnDrag(ClickReceiver startReceiver, ClickReceiver endReceiver)
			=> _orders.GiveOrder(startReceiver.Cell, endReceiver.Cell);

		private void OnClick(ClickReceiver clickReceiver) => OnCellClick(clickReceiver.Cell);

		private void OnCellClick(Cell cell)
		{
			_user.Player.ClickedCell = cell;
			DecideWhatToDoWith(cell).Invoke();
		}

		private Action DecideWhatToDoWith(Cell cell)
			=> cell.IsBelongTo(_user.Player.Id)
				? ShowRelevantMenu(cell)
				: DoNothing;

		private Action ShowRelevantMenu(Cell cell)
			=> cell.IsEmpty
				? _uiMediator.OpenWindow<BuildWindow>
				: _uiMediator.OpenWindow<BuildingWindow>;

		private static void DoNothing() { }
	}
}
using Zenject;

namespace Confrontation
{
	public class GameplayFieldInputHandler : FieldInputWithDragHandlerBase
	{
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly Orders _orders;

		protected override void OnCellsDrag(ClickReceiver startReceiver, ClickReceiver endReceiver)
			=> _orders.GiveOrder(startReceiver.Cell, endReceiver.Cell);

		protected override void OnCellClick(Cell cell)
		{
			if (cell.IsBelongTo(User.Player))
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
				_uiMediator.OpenWindow<BuildingInfoWindow>();
			}
		}
	}
}
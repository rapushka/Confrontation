using System;
using Zenject;

namespace Confrontation
{
	public class FieldClicksHandler : IInitializable
	{
		[Inject] private readonly Field _field;
		[Inject] private readonly User _user;
		[Inject] private readonly UiMediator _uiMediator;

		public void Initialize() { }

		private void OnCellMouseClick(Cell cell)
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
				? _uiMediator.ShowWindow<BuildWindow>
				: _uiMediator.ShowWindow<BuildingWindow>;

		private static void DoNothing() { }
	}
}
using Zenject;

namespace Confrontation
{
	public class LevelEditorFieldInputDirector : FieldInputDirectorBase
	{
		[Inject] private readonly LevelEditorTabsSystem _levelEditorTabsSystem;

		protected override void OnCellClick(Cell cell) => _levelEditorTabsSystem.CurrentPage.Handle(cell);
	}
}
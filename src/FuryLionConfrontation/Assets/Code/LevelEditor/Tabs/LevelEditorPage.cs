namespace Confrontation
{
	public abstract class LevelEditorPage : Page, IFieldClickHandler
	{
		public abstract void Handle(Cell clickedCell);
	}
}
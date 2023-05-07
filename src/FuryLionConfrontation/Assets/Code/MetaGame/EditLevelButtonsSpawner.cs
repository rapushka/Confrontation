using Zenject;

namespace Confrontation
{
	public class EditLevelButtonsSpawner : LevelButtonsSpawner
	{
		[Inject] private readonly LevelsForEditorPanel _levelsForEditorPanel;

		protected override LevelButtonBase Create(ILevel level)
		{
			var levelButton = base.Create(level);
			levelButton.transform.SetParent(_levelsForEditorPanel.LevelListRoot);
			return levelButton;
		}
	}
}
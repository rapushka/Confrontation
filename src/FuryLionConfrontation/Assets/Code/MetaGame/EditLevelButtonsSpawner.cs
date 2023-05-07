using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class EditLevelButtonsSpawner : LevelButtonsSpawner
	{
		[Inject] private readonly LevelsForEditorPanel _levelsForEditorPanel;

		protected override Transform Parent => _levelsForEditorPanel.LevelListRoot;

		protected override LevelButtonBase Create(ILevel level)
		{
			var levelButton = base.Create(level);
			levelButton.transform.SetParent(Parent);
			return levelButton;
		}
	}
}
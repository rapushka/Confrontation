using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class LevelSelectionButtonBase : ButtonBase
	{
		[Inject] protected readonly ILevel Level;
		[Inject] protected readonly User User;
		
		[SerializeField] private TextMeshProUGUI _textMesh;

		protected abstract ToSceneBase ToScene { get; }

		protected override async void OnButtonClick()
		{
			User.SelectedLevel = Level;
			await ToScene.Transfer();
		}
		
		public class Factory : PlaceholderFactory<ILevel, PlayLevelButton>
		{
			public PlayLevelButton Create(int levelNumber, ILevel level)
			{
				var levelButton = base.Create(level);
				levelButton._textMesh.text = levelNumber.ToString();
				return levelButton;
			}
		}

	}
}
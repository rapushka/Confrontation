using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class LevelButtonBase : ButtonBase
	{
		[Inject] protected readonly ILevel Level;
		[Inject] protected readonly User User;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private int _levelNumber;

		protected abstract ToSceneBase ToScene { get; }

		protected override async void OnButtonClick()
		{
			User.SelectedLevel = Level;
			User.SelectedLevelNumber = _levelNumber;
			await ToScene.Transfer();
		}

		public class Factory : PlaceholderFactory<ILevel, LevelButtonBase>
		{
			public T Create<T>(int levelNumber, ILevel level, bool isActive = true)
				where T : LevelButtonBase
			{
				var levelButton = base.Create(level);
				levelButton._textMesh.text = levelNumber.ToString();
				levelButton._levelNumber = levelNumber;
				levelButton.Interactable = isActive;
				return (T)levelButton;
			}
		}
	}
}
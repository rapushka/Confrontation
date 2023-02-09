using TMPro;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelButton : ButtonBase
	{
		[Inject] private readonly int _levelNumber;
		[Inject] private readonly Level _level;

		[Inject] private readonly ToGameplay _toGameplay;
		[Inject] private readonly User _user;

		[SerializeField] private TextMeshProUGUI _textMesh;

		private void Start()
		{
			_textMesh.text = _levelNumber.ToString();
		}

		protected override void OnButtonClick()
		{
			_user.SelectedLevel = _level;
			_toGameplay.Transfer();
		}

		public class Factory : PlaceholderFactory<int, Level, LevelButton> { }
	}
}
using UnityEngine;
using UnityEngine.UI;

namespace Confrontation
{
	public class LevelListItem : ButtonBase
	{
		[SerializeField] private Text _levelNameText;

		public string LevelName
		{
			get => _levelNameText.text;
			set => _levelNameText.text = value;
		}

		protected override void OnButtonClick() { }
	}
}
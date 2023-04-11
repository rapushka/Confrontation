using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class SpellButton : HoldButtonBase, IInitializable
	{
		[Inject] private readonly ISpell _spell;
		[Inject] private readonly GameplayUiMediator _uiMediator;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private Image _iconImage;

		public void Initialize()
		{
			_titleTextMesh.text = _spell.Title;
			_iconImage.sprite = _spell.Icon;
		}

		protected override void HandleClick()
		{
			Debug.Log("Cast spell");
			_uiMediator.CloseCurrentWindow();
		}

		protected override void HandleHold()
		{
			Debug.Log($"Spell Description:{_spell.Description}");
		}

		public class Factory : PlaceholderFactory<ISpell, SpellButton>
		{
			public override SpellButton Create(ISpell spell)
			{
				var spellButton = base.Create(spell);
				spellButton.Initialize();
				return spellButton;
			}
		}
	}
}
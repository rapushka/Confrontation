using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Confrontation
{
	public class SpellButton : HoldButtonBase, IInitializable
	{
		[Inject] private readonly ISpell _spell;
		[Inject] private ToolTip _toolTip;
		[Inject] private readonly GameplayUiMediator _uiMediator;
		[Inject] private readonly SpellCaster _spellCaster;

		[SerializeField] private TextMeshProUGUI _titleTextMesh;
		[SerializeField] private Image _iconImage;
		[SerializeField] private Button _button;

		public bool Interactable
		{
			set
			{
				_button.interactable = value;
				enabled = value;
			}
		}

		public void Initialize()
		{
			_titleTextMesh.text = $"{_spell.Title} — {_spell.ManaCoast}M";
			_iconImage.sprite = _spell.Icon;
		}

		protected override void HandleClick()
		{
			if (_spellCaster.TryCast(_spell))
			{
				_uiMediator.CloseCurrentWindow();
			}
			else
			{
				_uiMediator.OpenWindow<NotEnoughManaWindow>();
			}
		}

		protected override void HandleHold() => _toolTip.Show(withText: _spell.Description, on: transform.position);

		protected override void HandleRelease() => _toolTip.Hide();

		public class Factory : PlaceholderFactory<ISpell, ToolTip, SpellButton>
		{
			public SpellButton Create(ISpell spell, ToolTip toolTip, bool isActive = true)
			{
				var spellButton = base.Create(spell, toolTip);
				spellButton.Initialize();
				spellButton.Interactable = isActive;
				return spellButton;
			}
		}
	}
}
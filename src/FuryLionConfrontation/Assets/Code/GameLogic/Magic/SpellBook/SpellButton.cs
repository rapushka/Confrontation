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

		public void Initialize()
		{
			_titleTextMesh.text = _spell.Title;
			_iconImage.sprite = _spell.Icon;
		}

		protected override void HandleClick()
		{
			_spellCaster.Cast(_spell);
			_uiMediator.CloseCurrentWindow();
		}

		protected override void HandleHold() => _toolTip.Show(withText: _spell.Description, on: transform.position);

		protected override void OnRelease() => _toolTip.Hide();

		public class Factory : PlaceholderFactory<ISpell, ToolTip, SpellButton>
		{
			public override SpellButton Create(ISpell spell, ToolTip toolTip)
			{
				var spellButton = base.Create(spell, toolTip);
				spellButton.Initialize();
				return spellButton;
			}
		}
	}
}
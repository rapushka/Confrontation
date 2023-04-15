using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Spell", menuName = nameof(Confrontation) + "/Spell", order = 0)]
	public class SpellScriptableObject : ScriptableObject, ISpell
	{
		[SerializeField] private string _title;
		[SerializeField] private string _description;
		[SerializeField] private Sprite _icon;
		[SerializeField] private bool _isPermanent;
		[SerializeField] private float _duration;
		[SerializeField] private int _manaCoast;
		[SerializeField] private SpellType _spellType;

		public string Title => _title;

		public string Description => _description;

		public Sprite Icon { get => _icon; set => _icon = value; }

		public int       ManaCoast => _manaCoast;
		public SpellType SpellType => _spellType;

		public bool IsPermanent => _isPermanent;

		public float Duration => _duration;
	}
}
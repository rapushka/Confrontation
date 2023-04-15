using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Spell", menuName = nameof(Confrontation) + "/Spell", order = 0)]
	public class SpellScriptableObject : ScriptableObject, ISpell
	{
		[SerializeField] private string _title;
		[SerializeField] private string _description;
		[SerializeField] private Sprite _icon;
		[SerializeField] private SpellType _spellType;
		[SerializeField] private float _duration;
		[SerializeField] private int _manaCoast;

		public string Title => _title;

		public string Description => _description;

		public Sprite Icon => _icon;

		public SpellType SpellType => _spellType;

		public float Duration => _duration;

		public int ManaCoast => _manaCoast;
	}
}
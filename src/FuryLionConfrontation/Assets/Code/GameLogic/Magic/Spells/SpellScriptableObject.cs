using UnityEngine;

namespace Confrontation
{
	[CreateAssetMenu(fileName = "Spell", menuName = nameof(Confrontation) + "/Spell", order = 0)]
	public class SpellScriptableObject : ScriptableObject, ISpell
	{
		[field: Header("View")]
		[field: SerializeField] public string Title { get; private set; }

		[field: SerializeField] public string Description { get; private set; }

		[field: SerializeField] public Sprite Icon { get; private set; }

		[field: Header("Balance")]
		[field: SerializeField] public int ManaCoast { get; private set; }

		[field: SerializeField] public bool IsPermanent { get; private set; }

		[field: SerializeField] public float Duration { get; private set; }
	}
}
using UnityEngine;

namespace Confrontation
{
	public interface ISpell
	{
		string    Title       { get; }
		string    Description { get; }
		Sprite    Icon        { get; }
		int       ManaCoast   { get; }
		SpellType SpellType   { get; }
		float     Duration    { get; }
	}
}
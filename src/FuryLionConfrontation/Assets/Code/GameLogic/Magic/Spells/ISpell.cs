using System.Collections.Generic;
using UnityEngine;

namespace Confrontation
{
	public interface ISpell
	{
		string Title { get; }

		string Description { get; }

		Sprite Icon { get; }

		SpellType SpellType { get; }

		float Duration { get; }

		int ManaCoast { get; }

		IEnumerable<Influence> Influences { get; }
	}
}
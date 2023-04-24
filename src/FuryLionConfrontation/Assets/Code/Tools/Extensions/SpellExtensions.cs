using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public static class SpellExtensions
	{
		public static IEnumerable<TimedInfluence> AsTimedInfluences(this ISpell @this)
			=> @this.Influences.Select((i) => new TimedInfluence(@this, i));
	}
}
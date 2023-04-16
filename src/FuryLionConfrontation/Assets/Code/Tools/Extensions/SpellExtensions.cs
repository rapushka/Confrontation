using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public static class SpellExtensions
	{
		public static IEnumerable<TimedInfluence> AsTimedInfluences(this ISpell @this)
			=> @this.Influences.Select
			(
				(influence) => new TimedInfluence
				{
					Influence = influence.Influence,
					Target = influence.Target,
					TimeToLife = @this.SpellType is SpellType.Active ? 0f : @this.Duration,
					IsPermanent = @this.SpellType is SpellType.Permanent,
				}
			);
	}
}
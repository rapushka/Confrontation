using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class ActiveInfluences
	{
		private readonly List<TargetedInfluence> _influences = new();

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influences
			   .Where((i) => i.TargetForInfluence == withTarget)
			   .Aggregate(on, (v, i) => i.Influence.Apply(v));

		public void CastSpell(ISpell spell) => _influences.AddRange(spell.Influences);
	}
}
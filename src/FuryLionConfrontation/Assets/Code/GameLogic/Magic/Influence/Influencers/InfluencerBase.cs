using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class InfluencerBase : IInfluencer
	{
		private readonly List<TargetedInfluence> _influences = new();

		public virtual bool HasInfluenced => _influences.Any();

		protected virtual IEnumerable<TargetedInfluence> Influences => _influences;

		public virtual float Influence(float on, InfluenceTarget withTarget)
			=> WithTarget(withTarget).Aggregate(on, (v, ti) => ti.Influence.Apply(v));

		public virtual void CastSpell(ISpell spell) => AddInfluences(spell);

		protected virtual void AddInfluences(ISpell spell) => _influences.AddRange(spell.Influences);

		protected void Remove(TargetedInfluence influence) => _influences.Remove(influence);

		private IEnumerable<TargetedInfluence> WithTarget(InfluenceTarget target)
			=> Influences.Where((ti) => ti.Target == target);
	}
}
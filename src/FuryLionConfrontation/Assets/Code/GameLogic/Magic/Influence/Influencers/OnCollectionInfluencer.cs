using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class OnCollectionInfluencer<T> : InfluencerDecorator
	{
		protected abstract IEnumerable<T> Collection { get; }

		public virtual float Influence(float baseValue, InfluenceTarget withTarget, T @for)
			=> Collection.Contains(@for) ? base.Influence(baseValue, withTarget) : baseValue;

		public override float Influence(float baseValue, InfluenceTarget withTarget) => baseValue;
	}
}
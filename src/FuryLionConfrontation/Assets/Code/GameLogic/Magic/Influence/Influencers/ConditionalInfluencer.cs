using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class ConditionalInfluencer<T> : InfluencerDecorator
	{
		public override bool HasInfluenced => InfluencedElements.Any();

		protected HashSet<T> InfluencedElements { get; set; }

		public float Influence(float baseValue, InfluenceTarget withTarget, T @for)
		{
			InfluencedElements.Remove(@for, @if: IsDoesntMeetCondition);
			return InfluencedElements.Contains(@for) ? base.Influence(baseValue, withTarget) : baseValue;
		}

		protected abstract bool IsMeetsCondition(T element);

		private bool IsDoesntMeetCondition(T element) => IsMeetsCondition(element) == false;
	}
}
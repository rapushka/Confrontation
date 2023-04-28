using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class ConditionalInfluencer<T> : InfluencerBase
	{
		public bool HasInfluenced => InfluencedElements.Any();

		protected HashSet<T> InfluencedElements { get; set; }

		public float Influence(float baseValue, InfluenceTarget withTarget, T @for)
		{
			InfluencedElements.Remove(@for, @if: IsDoesntMeetCondition);
			return InfluencedElements.Contains(@for) ? base.Influence(baseValue, withTarget) : baseValue;
		}

		protected abstract bool IsMeetCondition(T element);

		private bool IsDoesntMeetCondition(T element) => IsMeetCondition(element) == false;
	}
}
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public abstract class SelectiveRemovalInfluencer<T> : OnCollectionInfluencer<T>
	{
		protected override InfluenceStatus CheckCondition()
			=> Collection.WithoutNulls().Any() ? InfluenceStatus.Neutral : InfluenceStatus.ForceDeath;

		protected override IEnumerable<T> Collection => InfluencedElements;

		protected HashSet<T> InfluencedElements { get; set; }

		public override float Influence(float baseValue, InfluenceTarget withTarget, T @for)
		{
			InfluencedElements.Remove(@for, @if: IsDoesntMeetCondition);
			return base.Influence(baseValue, withTarget, @for);
		}

		protected abstract bool IsMeetsCondition(T element);

		private bool IsDoesntMeetCondition(T element) => IsMeetsCondition(element) == false;
	}
}
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public abstract class ConditionalInfluencer<T> : IInfluencer
	{
		[Inject] private readonly IInfluencer _decoratee;

		public bool HasInfluenced => InfluencedElements.Any();

		protected HashSet<T> InfluencedElements { get; set; }

		public float Influence(float baseValue, InfluenceTarget withTarget, T @for)
		{
			InfluencedElements.Remove(@for, @if: IsDoesntMeetCondition);
			return InfluencedElements.Contains(@for) ? _decoratee.Influence(baseValue, withTarget) : baseValue;
		}

		public float Influence(float on, InfluenceTarget withTarget) => _decoratee.Influence(on, withTarget);

		public void CastSpell(ISpell spell) => _decoratee.CastSpell(spell);

		protected abstract bool IsMeetsCondition(T element);

		private bool IsDoesntMeetCondition(T element) => IsMeetsCondition(element) == false;
	}
}
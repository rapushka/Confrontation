using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class DuratedInfluencer : ConditionalInfluencer<float>, ILateTickable
	{
		[Inject] private readonly IInfluencer _decoratee;
		[Inject] private readonly ITimeService _time;

		private readonly List<(TargetedInfluence, float)> _timedInfluences = new();

		public void LateTick() { }

		protected override bool IsMeetsCondition(float duration) => duration >= 0f;

		public override float Influence(float on, InfluenceTarget withTarget)
		{
			return base.Influence(on, withTarget, 0f);
		}

		public class Factory : PlaceholderFactory<IInfluencer, DuratedInfluencer>
		{
			[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

			public DuratedInfluencer Create()
			{
				var influencerBase = _influencerBaseFactory.Create();
				var duratedInfluencer = Create(influencerBase);
				duratedInfluencer._timedInfluences.AddRange(influencerBase.Influences.Select((i) => (i, 0f)));
				
				return duratedInfluencer;
			}
		}
	}
}
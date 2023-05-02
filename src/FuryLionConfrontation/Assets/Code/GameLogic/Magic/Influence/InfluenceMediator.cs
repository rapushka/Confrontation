using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly OnAllUntilMovingUnitsInfluencer.Factory _onAllUntilMovingUnitsFactory;
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onAllMovingUnitsFactory;
		[Inject] private readonly OnOurUnitsInfluencer.Factory _ouOurUnitsFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;
		[Inject] private readonly PermanentInfluencer.Factory _permanentInfluencerFactory;
		[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

		private readonly HashSet<IInfluencer> _influencers = new();

		public InfluenceStatus Status => InfluenceStatus.ForceLive;

		public void CastSpell(ISpell spell)
		{
			foreach (var influence in spell.Influences)
			{
				IInfluencer influencerBase = _influencerBaseFactory.Create(influence);

				if (spell.SpellType is SpellType.Temporary)
				{
					influencerBase = _duratedInfluenceFactory.Create(spell.Duration, influencerBase);
				}

				if (spell.SpellType is SpellType.Permanent)
				{
					influencerBase = _permanentInfluencerFactory.Create(influencerBase);
				}

				influencerBase = ByCastingType(influence.CastingType, influencerBase);

				_influencers.Add(influencerBase);
			}
		}

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influencers.Aggregate(on, (current, i) => i.Influence(current, withTarget));

		public float Influence<T>(float on, InfluenceTarget withTarget, T @for)
			=> _influencers
			   .OfType<OnCollectionInfluencer<T>>()
			   .Aggregate(on, (current, i) => i.Influence(current, withTarget, @for));

		private IInfluencer ByCastingType(CastingType influenceCastingType, IInfluencer influencer)
			=> influenceCastingType switch
			{
				CastingType.Default             => influencer,
				CastingType.AllUntilMovingUnits => _onAllUntilMovingUnitsFactory.Create(influencer),
				CastingType.AllNowMovingUnits   => _onAllMovingUnitsFactory.Create(influencer),
				CastingType.OurUnits            => _ouOurUnitsFactory.Create(influencer),
				_                               => throw new ArgumentOutOfRangeException(),
			};

		private void ClearUnusedInfluencers()
			=> _influencers.RemoveWhere((i) => i.Status is InfluenceStatus.ForceDeath);
	}
}
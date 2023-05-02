using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly OnAllUntilMovingUnitsInfluencer.Factory _onAllUntilMovingUnitsInfluencerFactory;
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onAllMovingUnitsInfluencerFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;
		[Inject] private readonly PermanentInfluencer.Factory _permanentInfluencerFactory;
		[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

		private readonly HashSet<IInfluencer> _influencers = new();

		public InfluenceStatus Status => InfluenceStatus.ForceLive;

		private IEnumerable<IInfluencer> CommonInfluencers => _influencers;

		public void CastSpell(ISpell spell)
		{
			foreach (var influence in spell.Influences)
			{
				IInfluencer influencerBase = _influencerBaseFactory.Create(influence);

				influencerBase = spell.SpellType switch
				{
					SpellType.Temporary => _duratedInfluenceFactory.Create(spell.Duration, influencerBase),
					SpellType.Permanent => _permanentInfluencerFactory.Create(influencerBase),
					_                   => influencerBase,
				};

				influencerBase = AsInfluencerForTarget(influence, influencerBase);

				_influencers.Add(influencerBase);
			}
		}

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget)
			=> CommonInfluencers.Aggregate(on, (current, i) => i.Influence(current, withTarget));

		public float Influence<T>(float on, InfluenceTarget withTarget, T @for)
			=> _influencers
			   .OfType<OnCollectionInfluencer<T>>()
			   .Aggregate(on, (current, i) => i.Influence(current, withTarget, @for));

		private IInfluencer AsInfluencerForTarget(Influence influence, IInfluencer influencer)
			=> influence.Target switch
			{
				AllUntillMovingUnitsSpeed => _onAllUntilMovingUnitsInfluencerFactory.Create(influencer),
				AllNowMovingUnitsStrength => _onAllMovingUnitsInfluencerFactory.Create(influencer),
				OurUnitsSpeed             => throw new NotImplementedException(),
				AllFarmsBonus             => throw new NotImplementedException(),
				AllForgesBonus            => throw new NotImplementedException(),
				OurForgesBonus            => throw new NotImplementedException(),
				OurFarmsBonus             => throw new NotImplementedException(),
				OurGoldenMineProduceRate  => throw new NotImplementedException(),
				_                         => throw new ArgumentOutOfRangeException(),
			};

		private void ClearUnusedInfluencers()
			=> _influencers.RemoveWhere((i) => i.Status is InfluenceStatus.ForceDeath);
	}
}
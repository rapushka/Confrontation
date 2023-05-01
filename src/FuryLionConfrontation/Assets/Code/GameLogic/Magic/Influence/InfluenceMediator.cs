using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly OnAllUntilMovingUnitsInfluencer.Factory _onAllMovingUnitsInfluencerFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;
		[Inject] private readonly PermanentInfluencer.Factory _permanentInfluencerFactory;
		[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

		private readonly HashSet<IInfluencer> _duratedInfluencers = new();
		private readonly HashSet<IInfluencer> _conditionalInfluencers = new();
		private readonly HashSet<IInfluencer> _permanentInfluencers = new();

		public InfluenceStatus Status => InfluenceStatus.ForceLive;

		private IEnumerable<IInfluencer> CommonInfluencers => _duratedInfluencers.Concat(_permanentInfluencers);

		public void CastSpell(ISpell spell)
		{
			foreach (var influence in spell.Influences)
			{
				IInfluencer influencerBase = _influencerBaseFactory.Create(influence);

				if (spell.SpellType is SpellType.Temporary)
				{
					influencerBase = _duratedInfluenceFactory.Create(spell.Duration, influencerBase);
					_duratedInfluencers.Add(influencerBase);
				}

				if (spell.SpellType is SpellType.Active)
				{
					influencerBase = AsInfluencerForTarget(influence, influencerBase);
					_conditionalInfluencers.Add(influencerBase);
				}

				if (spell.SpellType is SpellType.Permanent)
				{
					influencerBase = _permanentInfluencerFactory.Create(influencerBase);
					_permanentInfluencers.Add(influencerBase);
				}
			}
		}

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget)
			=> CommonInfluencers.Aggregate(on, (current, i) => i.Influence(current, withTarget));

		public float Influence<T>(float on, InfluenceTarget withTarget, T @for)
		{
			var fromConditional = _conditionalInfluencers
			                      .OfType<OnUntilInCollectionInfluencer<T>>()
			                      .Aggregate(on, (x, i) => i.Influence(baseValue: x, withTarget, @for));

			return Influence(fromConditional, withTarget);
		}

		private IInfluencer AsInfluencerForTarget(Influence influence, IInfluencer influencer)
			=> influence.Target switch
			{
				AllUntillMovingUnitsSpeed => _onAllMovingUnitsInfluencerFactory.Create(influencer),
				AllNowMovingUnitsStrength => throw new NotImplementedException(),
				OurUnitsSpeed             => throw new NotImplementedException(),
				AllFarmsBonus             => throw new NotImplementedException(),
				AllForgesBonus            => throw new NotImplementedException(),
				OurForgesBonus            => throw new NotImplementedException(),
				OurFarmsBonus             => throw new NotImplementedException(),
				OurGoldenMineProduceRate  => throw new NotImplementedException(),
				_                         => throw new ArgumentOutOfRangeException(),
			};

		private void ClearUnusedInfluencers()
		{
			_conditionalInfluencers.RemoveWhere((i) => i.Status == InfluenceStatus.ForceDeath);
			_duratedInfluencers.RemoveWhere((i) => i.Status == InfluenceStatus.ForceDeath);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onAllMovingUnitsInfluencerFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;
		[Inject] private readonly PermanentInfluencer.Factory _permanentInfluencerFactory;

		private readonly HashSet<IInfluencer> _duratedInfluencers = new();
		private readonly HashSet<IInfluencer> _conditionalInfluencers = new();
		private readonly HashSet<IInfluencer> _permanentInfluencers = new();

		public InfluenceStatus Status => InfluenceStatus.Neutral;

		private IEnumerable<IInfluencer> AllInfluencers => _duratedInfluencers
		                                                   .Concat(_conditionalInfluencers)
		                                                   .Concat(_permanentInfluencers);

		public void CastSpell(ISpell spell)
		{
			switch (spell.SpellType)
			{
				case SpellType.Temporary:
					_duratedInfluencers.AddRange(spell.Influences.Select((i) => AsDurated(i, spell.Duration)));
					break;
				case SpellType.Active:
					_conditionalInfluencers.AddRange(spell.Influences.Select(AsInfluencerForTarget));
					break;
				case SpellType.Permanent:
					_permanentInfluencers.AddRange(spell.Influences.Select(_permanentInfluencerFactory.Create));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public float Influence(float on, InfluenceTarget withTarget)
			=> AllInfluencers.Aggregate(on, (current, i) => i.Influence(current, withTarget));

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence<T>(float on, InfluenceTarget withTarget, T @for)
			=> _conditionalInfluencers
			   .OfType<SelectiveRemovalInfluencer<T>>()
			   .Aggregate(on, (x, i) => i.Influence(baseValue: x, withTarget, @for));

		private void ClearUnusedInfluencers()
		{
			_conditionalInfluencers.RemoveWhere((i) => i.Status == InfluenceStatus.ForceDeath);
			_duratedInfluencers.RemoveWhere((i) => i.Status == InfluenceStatus.ForceDeath);
		}

		private IInfluencer AsInfluencerForTarget(Influence influence)
			=> influence.Target switch
			{
				AllMovingUnitsSpeed
					or AllMovingUnitsStrength => _onAllMovingUnitsInfluencerFactory.Create(influence),
				OurUnitsSpeed            => throw new NotImplementedException(),
				AllFarmsBonus            => throw new NotImplementedException(),
				OurFarmsBonus            => throw new NotImplementedException(),
				AllForgesBonus           => throw new NotImplementedException(),
				OurForgesBonus           => throw new NotImplementedException(),
				OurGoldenMineProduceRate => throw new NotImplementedException(),
				var _                    => throw new ArgumentOutOfRangeException(),
			};

		private DuratedInfluencer AsDurated(Influence influence, float duration)
			=> _duratedInfluenceFactory.Create(influence, duration);
	}
}
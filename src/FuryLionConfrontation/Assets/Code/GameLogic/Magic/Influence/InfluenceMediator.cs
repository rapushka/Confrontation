using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onAllMovingUnitsInfluencerFactory;
		[Inject] private readonly DuratedInfluencer.Factory _duratedInfluenceFactory;

		private readonly HashSet<DuratedInfluencer> _duratedInfluencers = new();
		private readonly HashSet<IInfluencer> _conditionalInfluencers = new();

		public bool IsAlive => true;

		private IEnumerable<IInfluencer> AllInfluencers => _duratedInfluencers.Concat(_conditionalInfluencers);

		public void CastSpell(ISpell spell)
		{
			switch (spell.SpellType)
			{
				case SpellType.Temporary:
					_duratedInfluencers.AddRange(spell.Influences.Select((i) => AsDurated(i, spell)));
					break;
				case SpellType.Active:
					_conditionalInfluencers.AddRange(spell.Influences.Select(AsInfluencerForTarget));
					break;
				case SpellType.Permanent:
					Debug.LogError("TODO: Permanent spells");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public float Influence(float on, InfluenceTarget withTarget)
			=> AllInfluencers.Aggregate(on, (current, i) => i.Influence(current, withTarget));

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget, UnitsSquad @for)
			=> _conditionalInfluencers
			   .OfType<OnAllMovingUnitsInfluencer>()
			   .Aggregate(on, (x, i) => i.Influence(baseValue: x, withTarget, @for));

		private void ClearUnusedInfluencers()
		{
			_conditionalInfluencers.RemoveWhere((i) => i.IsAlive == false);
			_duratedInfluencers.RemoveWhere((i) => i.IsAlive == false);
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

		private DuratedInfluencer AsDurated(Influence influence, ISpell spell)
			=> _duratedInfluenceFactory.Create(influence, spell.Duration);
	}
}
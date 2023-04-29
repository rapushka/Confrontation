using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static Confrontation.InfluenceTarget;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly InfluencesWithDuration _influencesWithDuration;
		[Inject] private readonly OnAllMovingUnitsInfluencer.Factory _onAllMovingUnitsInfluencerFactory;

		private readonly HashSet<IInfluencer> _conditionalInfluencers = new();

		public void CastSpell(ISpell spell)
		{
			switch (spell.SpellType)
			{
				case SpellType.Temporary:
					_influencesWithDuration.CastSpell(spell);
					break;
				case SpellType.Active:
				{
					CastActiveSpell(spell);
					break;
				}
				case SpellType.Permanent:
					Debug.LogError("TODO: Permanent spells");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool HasInfluenced => _influencesWithDuration.HasInfluenced
		                             || _conditionalInfluencers.Any((i) => i.HasInfluenced);

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influencesWithDuration.Influence(on, withTarget);

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget, UnitsSquad @for)
			=> _conditionalInfluencers
			   .OfType<OnAllMovingUnitsInfluencer>()
			   .Aggregate(on, (x, i) => i.Influence(baseValue: x, withTarget, @for));

		private void ClearUnusedInfluencers()
			=> _conditionalInfluencers.RemoveWhere((i) => i.HasInfluenced == false);

		private void CastActiveSpell(ISpell spell)
		{
			foreach (var influence in spell.Influences)
			{
				var newInfluencer = CreateInfluencerForTarget(influence.Target);
				newInfluencer.CastSpell(spell);
				_conditionalInfluencers.Add(newInfluencer);
			}
		}

		private IInfluencer CreateInfluencerForTarget(InfluenceTarget target)
			=> target switch
			{
				AllMovingUnitsSpeed
					or AllMovingUnitsStrength => _onAllMovingUnitsInfluencerFactory.Create(),
				OurUnitsSpeed            => throw new NotImplementedException(),
				AllFarmsBonus            => throw new NotImplementedException(),
				OurFarmsBonus            => throw new NotImplementedException(),
				AllForgesBonus           => throw new NotImplementedException(),
				OurForgesBonus           => throw new NotImplementedException(),
				OurGoldenMineProduceRate => throw new NotImplementedException(),
				var _                    => throw new ArgumentOutOfRangeException(),
			};
	}
}
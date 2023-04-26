using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly InfluencesWithDuration _influencesWithDuration;
		[Inject] private readonly OnMovingUnitsInfluencer.Factory _onMovingUnitsInfluencerFactory;

		private readonly HashSet<OnMovingUnitsInfluencer> _onMovingUnitsInfluencers = new();

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

		public float Influence(float on, InfluenceTarget withTarget)
			=> _influencesWithDuration.Influence(on, withTarget);

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget, UnitsSquad @for)
			=> _onMovingUnitsInfluencers.Aggregate(on, (x, i) => i.Influence(on: x, withTarget, @for));

		private void ClearUnusedInfluencers()
			=> _onMovingUnitsInfluencers.RemoveWhere((i) => i.HasInfluenced == false);

		private void CastActiveSpell(ISpell spell)
		{
			var newInfluencer = _onMovingUnitsInfluencerFactory.Create();
			newInfluencer.CastSpell(spell);
			_onMovingUnitsInfluencers.Add(newInfluencer);
		}
	}
}
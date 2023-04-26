using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer, ILateTickable
	{
		[Inject] private readonly InfluencesWithDuration _influencesWithDuration;
		[Inject] private readonly OnMovingUnitsInfluencer.Factory _onMovingUnitsInfluencerFactory;

		private readonly List<OnMovingUnitsInfluencer> _onMovingUnitsInfluencers = new();

		public void CastSpell(ISpell spell)
		{
			if (spell.SpellType is SpellType.Temporary)
			{
				_influencesWithDuration.CastSpell(spell);
			}
			else if (spell.SpellType is SpellType.Active)
			{
				var newInfluencer = _onMovingUnitsInfluencerFactory.Create();
				newInfluencer.CastSpell(spell);
				_onMovingUnitsInfluencers.Add(newInfluencer);
			}
		}

		public float Influence(float on, InfluenceTarget withTarget) 
			=> _influencesWithDuration.Influence(on, withTarget);

		public void LateTick() => ClearUnusedInfluencers();

		public float Influence(float on, InfluenceTarget withTarget, UnitsSquad @for) 
			=> _onMovingUnitsInfluencers.Aggregate(on, (x, i) => i.Influence(on: x, withTarget, @for));

		private void ClearUnusedInfluencers() => _onMovingUnitsInfluencers.RemoveIf((i) => i.HasInfluenced == false);
	}
}
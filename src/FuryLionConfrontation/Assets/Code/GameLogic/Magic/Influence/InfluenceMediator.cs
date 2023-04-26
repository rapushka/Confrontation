using Zenject;

namespace Confrontation
{
	public class InfluenceMediator : IInfluencer
	{
		[Inject] private readonly InfluencesWithDuration _influencesWithDuration;

		public void CastSpell(ISpell spell)
		{
			if (spell.SpellType is SpellType.Temporary)
			{
				_influencesWithDuration.CastSpell(spell);
			}
		}

		public float Influence(float on, InfluenceTarget withTarget) 
			=> _influencesWithDuration.Influence(on, withTarget);
	}
}
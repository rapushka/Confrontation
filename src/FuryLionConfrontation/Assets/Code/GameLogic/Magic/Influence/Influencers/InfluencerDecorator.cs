using Zenject;

namespace Confrontation
{
	public abstract class InfluencerDecorator : IInfluencer
	{
		[Inject] private readonly IInfluencer _decoratee;

		public virtual bool HasInfluenced => _decoratee.HasInfluenced;

		public virtual float Influence(float on, InfluenceTarget withTarget) => _decoratee.Influence(on, withTarget);

		public virtual void CastSpell(ISpell spell) => _decoratee.CastSpell(spell);
	}
}
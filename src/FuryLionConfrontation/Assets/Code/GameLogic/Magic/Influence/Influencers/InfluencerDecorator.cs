using Zenject;

namespace Confrontation
{
	public abstract class InfluencerDecorator : IInfluencer
	{
		[Inject] private readonly IInfluencer _decoratee;

		public virtual bool IsAlive => _decoratee.IsAlive;

		public virtual float Influence(float on, InfluenceTarget withTarget) => _decoratee.Influence(on, withTarget);
	}
}
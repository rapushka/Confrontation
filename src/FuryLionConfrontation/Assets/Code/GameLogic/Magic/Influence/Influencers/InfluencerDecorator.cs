using Zenject;

namespace Confrontation
{
	public abstract class InfluencerDecorator : IInfluencer
	{
		[Inject] private readonly IInfluencer _decoratee;

		public virtual InfluenceStatus Status
			=> _decoratee.Status is InfluenceStatus.Neutral
				? CheckCondition()
				: _decoratee.Status;

		protected abstract InfluenceStatus CheckCondition();

		public virtual float Influence(float on, InfluenceTarget withTarget) => _decoratee.Influence(on, withTarget);
	}
}
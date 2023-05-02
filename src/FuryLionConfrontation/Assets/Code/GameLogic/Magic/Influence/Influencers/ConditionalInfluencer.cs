namespace Confrontation
{
	public abstract class ConditionalInfluencer<T> : InfluencerDecorator
	{
		protected override InfluenceStatus CheckCondition() => InfluenceStatus.Neutral;

		public virtual float Influence(float baseValue, InfluenceTarget withTarget, T @for)
			=> IsMatchCondition(@for) ? base.Influence(baseValue, withTarget) : baseValue;

		protected abstract bool IsMatchCondition(T item);
	}
}
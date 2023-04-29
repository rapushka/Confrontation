using Zenject;

namespace Confrontation
{
	public class DuratedInfluencer : InfluencerDecorator, ITickable
	{
		[Inject] private float _duration;
		[Inject] private readonly ITimeService _time;

		protected override InfluenceStatus CheckCondition()
			=> _duration > 0 ? InfluenceStatus.Neutral : InfluenceStatus.ForceDeath;

		public void Tick() => _duration -= _time.DeltaTime;

		public class Factory : PlaceholderFactory<float, IInfluencer, DuratedInfluencer>
		{
			[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

			public DuratedInfluencer Create(Influence influence, float duration)
				=> Create(duration, _influencerBaseFactory.Create(influence));

			public override DuratedInfluencer Create(float duration, IInfluencer decoratee)
				=> base.Create(duration, decoratee);
		}
	}
}
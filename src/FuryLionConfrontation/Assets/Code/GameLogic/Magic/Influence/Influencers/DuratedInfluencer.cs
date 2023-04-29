using Zenject;

namespace Confrontation
{
	public class DuratedInfluencer : InfluencerDecorator, ITickable
	{
		[Inject] private float _duration;
		[Inject] private readonly ITimeService _time;

		public override bool IsAlive => _duration > 0 || base.IsAlive;

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
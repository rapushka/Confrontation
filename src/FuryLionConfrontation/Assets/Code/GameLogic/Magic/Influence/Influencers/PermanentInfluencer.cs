using Zenject;

namespace Confrontation
{
	public class PermanentInfluencer : InfluencerDecorator
	{
		public override InfluenceStatus Status => CheckCondition();

		protected override InfluenceStatus CheckCondition() => InfluenceStatus.ForceLive;

		public class Factory : PlaceholderFactory<IInfluencer, PermanentInfluencer>
		{
			[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

			public PermanentInfluencer Create(Influence influence) => Create(_influencerBaseFactory.Create(influence));

			public override PermanentInfluencer Create(IInfluencer decoratee) => base.Create(decoratee);
		}
	}
}
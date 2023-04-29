using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnAllMovingUnitsInfluencer : ConditionalInfluencer<UnitsSquad>
	{
		protected override bool IsMeetsCondition(UnitsSquad element) => element.IsMoving;

		public class Factory : PlaceholderFactory<IInfluencer, OnAllMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;
			[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

			public OnAllMovingUnitsInfluencer Create(Influence influence)
				=> Create(_influencerBaseFactory.Create(influence));

			public override OnAllMovingUnitsInfluencer Create(IInfluencer decoratee)
			{
				var influencer = base.Create(decoratee);
				influencer.InfluencedElements = _field.MovingUnits.ToHashSet();
				return influencer;
			}
		}
	}
}
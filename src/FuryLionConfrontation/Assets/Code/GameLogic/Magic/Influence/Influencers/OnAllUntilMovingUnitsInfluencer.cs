using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnAllUntilMovingUnitsInfluencer : OnUntilInCollectionInfluencer<UnitsSquad>
	{
		protected override bool IsMeetsCondition(UnitsSquad element) => element.IsMoving;

		public class Factory : PlaceholderFactory<IInfluencer, OnAllUntilMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;
			[Inject] private readonly InfluencerBase.Factory _influencerBaseFactory;

			public OnAllUntilMovingUnitsInfluencer Create(Influence influence)
				=> Create(_influencerBaseFactory.Create(influence));

			public override OnAllUntilMovingUnitsInfluencer Create(IInfluencer decoratee)
			{
				var influencer = base.Create(decoratee);
				influencer.InfluencedElements = _field.MovingUnits.ToHashSet();
				return influencer;
			}
		}
	}
}
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnMovingUnitsInfluencer : ConditionalInfluencer<UnitsSquad>
	{
		protected override bool IsMeetCondition(UnitsSquad element) => element.IsMoving;

		public class Factory : PlaceholderFactory<OnMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnMovingUnitsInfluencer Create()
			{
				var influencer = base.Create();
				influencer.InfluencedElements = _field.AllUnits.Except(_field.LocatedUnits).ToHashSet();
				return influencer;
			}
		}
	}
}
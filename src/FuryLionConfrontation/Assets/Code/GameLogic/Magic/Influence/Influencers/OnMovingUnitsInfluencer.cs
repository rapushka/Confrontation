using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnMovingUnitsInfluencer : ConditionalInfluencer<UnitsSquad>
	{
		protected override bool IsMeetsCondition(UnitsSquad element) => element.IsMoving;

		public class Factory : PlaceholderFactory<OnMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnMovingUnitsInfluencer Create()
				=> base.Create().With((i) => i.InfluencedElements = _field.MovingUnits.ToHashSet());
		}
	}
}
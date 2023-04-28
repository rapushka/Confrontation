using System.Linq;
using Zenject;

namespace Confrontation
{
	public class OnAllMovingUnitsInfluencer : ConditionalInfluencer<UnitsSquad>
	{
		protected override bool IsMeetsCondition(UnitsSquad element) => element.IsMoving;

		public class Factory : PlaceholderFactory<OnAllMovingUnitsInfluencer>
		{
			[Inject] private readonly IField _field;

			public override OnAllMovingUnitsInfluencer Create()
				=> base.Create().With((i) => i.InfluencedElements = _field.MovingUnits.ToHashSet());
		}
	}
}
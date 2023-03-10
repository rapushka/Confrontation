using System.Linq;

namespace Confrontation
{
	public class AcceleratedUnitMovementDecorator : UnitMovement
	{
		protected override float Speed
		{
			get
			{
				var ownerPlayerId = OwnerPlayerId;

				var baseSpeed = base.Speed;

				var speed = Field.Buildings
				                 .OfType<Stable>()
				                 .Where((f) => f.OwnerPlayerId == ownerPlayerId)
				                 .Aggregate(baseSpeed, DecreaseOnCoefficient);

				return speed;
			}
		}

		private float DecreaseOnCoefficient(float current, Stable stable)
			=> current + stable.CurrentLevelStats.UnitsAccelerationCoefficient;
	}
}
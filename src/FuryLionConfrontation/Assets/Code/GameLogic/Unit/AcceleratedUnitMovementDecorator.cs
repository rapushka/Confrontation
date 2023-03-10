using System.Linq;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class AcceleratedUnitMovementDecorator : UnitMovement
	{
		[Inject] private readonly IField _field;

		[SerializeField] private UnitsSquad _squad;

		protected override float Speed
		{
			get
			{
				var ownerPlayerId = _squad.OwnerPlayerId;
				var baseSpeed = base.Speed;

				var speed = _field.Buildings
				                  .OfType<Stable>()
				                  .Where((f) => f.OwnerPlayerId == ownerPlayerId)
				                  .Aggregate(baseSpeed, Accelerate);

				return speed;
			}
		}

		private static float Accelerate(float currentSpeed, Stable stable)
			=> currentSpeed + stable.CurrentLevelStats.UnitsAccelerationCoefficient;
	}
}
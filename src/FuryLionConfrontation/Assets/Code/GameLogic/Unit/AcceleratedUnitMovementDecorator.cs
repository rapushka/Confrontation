using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class AcceleratedUnitMovementDecorator : UnitMovement
	{
		[Inject] private readonly IField _field;

		[SerializeField] private UnitsSquad _squad;

		protected override float Speed
			=> _field.Buildings.InfluenceFloat<Stable>(base.Speed, _squad.OwnerPlayerId, Accelerate);

		private static float Accelerate(float currentSpeed, Stable stable)
			=> currentSpeed + stable.CurrentLevelStats.UnitsAccelerationCoefficient;
	}
}
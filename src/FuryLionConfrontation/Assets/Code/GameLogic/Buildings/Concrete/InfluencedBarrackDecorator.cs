using System.Linq;

namespace Confrontation
{
	public class InfluencedBarrackDecorator : Barrack
	{
		public override float CoolDownDuration
		{
			get
			{
				var ownerPlayerId = OwnerPlayerId;

				var baseDuration = base.CoolDownDuration;
				var duration = Field.Buildings
				                    .Union(Field.StashedBuildings)
				                    .InfluenceFloat<Farm>(baseDuration, ownerPlayerId, DecreaseOnCoefficient);

				return duration.Clamp(min: MinAcceleratedCoolDown);
			}
		}

		private static float DecreaseOnCoefficient(float current, Farm farm)
			=> current - farm.CurrentLevelStats.SpawnAccelerationCoefficient;
	}
}
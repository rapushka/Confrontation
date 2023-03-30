using System;
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
				                    .OfType<Farm>()
				                    .Where((f) => f.OwnerPlayerId == ownerPlayerId)
				                    .Aggregate(baseDuration, DecreaseOnCoefficient);

				return Math.Max(duration, MinAcceleratedCoolDown);
			}
		}

		private float DecreaseOnCoefficient(float current, Farm farm)
			=> current - farm.CurrentLevelStats.SpawnAccelerationCoefficient;
	}
}
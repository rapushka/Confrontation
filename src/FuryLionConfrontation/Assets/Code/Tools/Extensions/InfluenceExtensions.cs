using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public static class InfluenceExtensions
	{
		public static float InfluenceFloat<TBuilding>
		(
			this IEnumerable<Building> @this,
			float baseValue,
			int ownerId,
			Func<float, TBuilding, float> influence
		)
			where TBuilding : Building
			=> ownerId != Constants.NeutralRegion
				? @this.OfType<TBuilding>().Where((f) => f.OwnerPlayerId == ownerId).Aggregate(baseValue, influence)
				: baseValue;

		public static bool AnyNegativeInfluence(this IEnumerable<TimedInfluence> @this) 
			=> @this.Any((ti) => ti.Influence.Coefficient < 0);
	}
}
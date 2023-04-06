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
			=> @this.OfType<TBuilding>().Where((f) => f.OwnerPlayerId == ownerId).Aggregate(baseValue, influence);
	}
}
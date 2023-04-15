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

		public static float Influence(this IEnumerable<Influence> @this, float baseValue)
			=> @this.Aggregate(baseValue, (v, i) => i.Apply(v));

		public static float Influence(this IEnumerable<TargetedInfluence> @this, float baseValue)
			=> @this.Aggregate(baseValue, (v, i) => i.Influence.Apply(v));

		public static IEnumerable<TargetedInfluence> WithTarget
		(
			this IEnumerable<TargetedInfluence> @this,
			InfluenceTarget target
		)
			=> @this.Where((i) => i.TargetForInfluence == target);
	}
}
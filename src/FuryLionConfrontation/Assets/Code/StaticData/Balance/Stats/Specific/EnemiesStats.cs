using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class EnemiesStats : IStats
	{
		[field: SerializeField] public Range DurationRangeBetweenActions { get; private set; }

		[field: SerializeField] public WeightedCollection<Building.Data> BuildingsPriority { get; private set; }
	}
}
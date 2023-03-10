using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class StableStats : IPriced, ILeveled<StableLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<StableLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class StableLevelStats : IStats
	{
		[field: SerializeField] public float UnitsAccelerationCoefficient { get; private set; }
	}
}
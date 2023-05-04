using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class FarmStats : IPriced, ILeveled<FarmLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<FarmLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class FarmLevelStats : IStats
	{
		[field: SerializeField] public virtual float SpawnAccelerationCoefficient { get; private set; }
	}
}
using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class BarrackStats : IPriced, ILeveled<BarrackLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<BarrackLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class BarrackLevelStats : GeneratorStatsBase
	{
		[field: SerializeField] public float MinAcceleratedCoolDown { get; private set; }
	}
}
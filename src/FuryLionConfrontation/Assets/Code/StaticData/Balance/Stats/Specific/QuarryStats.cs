using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class QuarryStats : ILeveled<QuarryLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<QuarryLevelStats> LeveledStats { get; private set; }
	}

	
	[Serializable]
	public class QuarryLevelStats : IStats
	{
		[field: SerializeField] public float IncreasesDamageAbsorptionRate { get; private set; }
	}
}
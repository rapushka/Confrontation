using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class ForgeStats : ILeveled<ForgeLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<ForgeLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class ForgeLevelStats : IStats
	{
		[field: SerializeField] public float CombatStrengthIncreasesRate { get; private set; }
	}
}
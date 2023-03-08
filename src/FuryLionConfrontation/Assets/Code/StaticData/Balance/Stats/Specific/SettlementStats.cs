using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class SettlementStats : ILeveled<SettlementLevelStats>, IStats
	{
		[field: SerializeField] public LeveledStats<SettlementLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class SettlementLevelStats : GeneratorStatsBase
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
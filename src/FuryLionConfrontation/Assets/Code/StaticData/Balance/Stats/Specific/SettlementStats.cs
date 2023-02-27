using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class SettlementStats : ILeveled<SettlementLevelStats>, IStats
	{
		[field: SerializeField] public LeveledStats<SettlementLevelStats> LeveledStats { get; private set; }
	}
}
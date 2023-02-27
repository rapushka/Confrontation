using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class SettlementLevelStats : GeneratorStatsBase
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
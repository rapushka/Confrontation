using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class BarrackStats : IPriced, ILeveled<GeneratorStatsBase>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<GeneratorStatsBase> LeveledStats { get; private set; }
	}
}
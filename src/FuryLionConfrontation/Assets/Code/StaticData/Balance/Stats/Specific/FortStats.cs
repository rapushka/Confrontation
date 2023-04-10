using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class FortStats : ILeveled<FortLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<FortLevelStats> LeveledStats { get; private set; }
	}

	[Serializable]
	public class FortLevelStats : IStats
	{
		[field: SerializeField] public float AdditionalDefenceModifier { get; private set; }
	}
}
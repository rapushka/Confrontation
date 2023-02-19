using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class VillageStats : ILeveled<VillageLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<VillageLevelStats> LeveledStats { get; private set; }
	}
}
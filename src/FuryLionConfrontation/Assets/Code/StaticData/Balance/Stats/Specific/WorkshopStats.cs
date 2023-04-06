using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class WorkshopStats : ILeveled<WorkshopLevelStats>, IStats
	{
		[field: SerializeField] public int Price { get; private set; }

		[field: SerializeField] public LeveledStats<WorkshopLevelStats> LeveledStats { get; private set; }
	}

	
	[Serializable]
	public class WorkshopLevelStats : IStats
	{
		[field: SerializeField] public float DecreaseEnemyDamageAbsorptionRate { get; private set; }
	}
}
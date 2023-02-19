using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class VillageStats : GeneratorBaseStats
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
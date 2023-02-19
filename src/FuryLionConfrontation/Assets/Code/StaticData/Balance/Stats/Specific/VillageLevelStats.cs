using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class VillageLevelStats : GeneratorStatsBase
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
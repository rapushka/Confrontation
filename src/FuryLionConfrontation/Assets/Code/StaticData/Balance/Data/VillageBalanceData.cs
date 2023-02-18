using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class VillageBalanceData : GeneratorBaseBalanceData
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
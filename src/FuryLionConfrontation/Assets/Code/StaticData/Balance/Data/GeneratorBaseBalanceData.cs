using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public abstract class GeneratorBaseBalanceData : IBalanceData
	{
		[field: SerializeField] public float GenerationCoolDown { get; private set; }

		[field: SerializeField] public int GenerationAmount { get; private set; }
	}

	[Serializable] public class GoldenMineBalanceData : GeneratorBaseBalanceData { }

	[Serializable] public class BarrackBalanceData : GeneratorBaseBalanceData { }

	[Serializable]
	public class VillageBalanceData : GeneratorBaseBalanceData
	{
		[field: SerializeField] public int MaxInGarrisonNumber { get; private set; }
	}
}
using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class GoldenMineBalanceData : CooledDownBalanceData
	{
		[field: SerializeField] public int ProduceAmount { get; private set; }
	}
}
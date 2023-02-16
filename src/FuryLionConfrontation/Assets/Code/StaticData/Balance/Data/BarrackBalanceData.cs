using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class BarrackBalanceData : CooledDownBalanceData
	{
		[field: SerializeField] public int SpawnAmount { get; private set; }
	}
}
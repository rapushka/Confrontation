using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class CooledDownBalanceData : IBalanceData
	{
		[field: SerializeField] public float CoolDownDuration { get; private set; }
	}
}
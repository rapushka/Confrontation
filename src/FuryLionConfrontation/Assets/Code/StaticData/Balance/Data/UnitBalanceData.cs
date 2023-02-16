using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class UnitBalanceData : IBalanceData
	{
		[field: SerializeField] public float BaseSpeed { get; private set; }
	}
}
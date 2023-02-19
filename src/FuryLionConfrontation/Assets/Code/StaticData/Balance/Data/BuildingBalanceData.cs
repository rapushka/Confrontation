using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public abstract class BuildingBalanceData : IBalanceData
	{
		[field: SerializeField] public int Price { get; private set; }
	}
}
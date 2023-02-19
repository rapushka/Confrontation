using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public abstract class BuildingStats : IStats
	{
		[field: SerializeField] public int Price { get; private set; }
	}
}
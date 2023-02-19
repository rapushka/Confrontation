using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class UnitStats : IStats
	{
		[field: SerializeField] public float BaseSpeed { get; private set; }
	}
}
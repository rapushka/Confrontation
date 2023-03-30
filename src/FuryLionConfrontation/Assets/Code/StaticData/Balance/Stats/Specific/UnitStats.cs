using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public class UnitStats : IStats
	{
		[field: SerializeField] public float BaseSpeed { get; private set; }

		[field: SerializeField] public float BaseStrength { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float DefenseModifier { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float AttackModifier { get; private set; }
	}
}
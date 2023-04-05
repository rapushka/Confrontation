using System;
using UnityEngine;

namespace Confrontation
{
	public interface IUnitStats : IStats
	{
		float              BaseSpeed                  { get; }
		float              BaseStrength               { get; }
		float              DefenseModifier            { get; }
		float              AttackModifier             { get; }
		FloatToIntStrategy ConvertDamageToUnitsQuantity { get; }
	}

	[Serializable]
	public class UnitStats : IUnitStats
	{
		[field: SerializeField] public float BaseSpeed { get; private set; }

		[field: SerializeField] public float BaseStrength { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float DefenseModifier { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float AttackModifier { get; private set; }

		[field: SerializeField] public FloatToIntStrategy ConvertDamageToUnitsQuantity { get; private set; }
	}
}
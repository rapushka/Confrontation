using System;
using UnityEngine;

namespace Confrontation
{
	public interface IUnitStats : IStats
	{
		float BaseSpeed { get; }

		float BaseStrength { get; }

		float BaseArmourMultiplier { get; }

		float DefencePierceRate { get; }

		float UnitMaxHp { get; }

		float DefenseModifier { get; }

		float AttackModifier { get; }
	}

	[Serializable]
	public class UnitStats : IUnitStats
	{
		[field: SerializeField] public float BaseSpeed { get; private set; }

		[field: SerializeField] public float BaseStrength { get; private set; }

		[field: SerializeField] public float BaseArmourMultiplier { get; private set; }

		[field: SerializeField] public float DefencePierceRate { get; private set; }

		[field: SerializeField] public float UnitMaxHp { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float DefenseModifier { get; private set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float AttackModifier { get; private set; }
	}
}
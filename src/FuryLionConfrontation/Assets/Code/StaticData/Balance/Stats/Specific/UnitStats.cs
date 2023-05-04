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
		[field: SerializeField] public float BaseSpeed { get; set; }

		[field: SerializeField] public float BaseStrength { get; set; }

		[field: SerializeField] public float BaseArmourMultiplier { get; set; }

		[field: SerializeField] public float DefencePierceRate { get; set; }

		[field: SerializeField] public float UnitMaxHp { get; set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float DefenseModifier { get; set; }

		[field: Range(0f, 1f)] [field: SerializeField] public float AttackModifier { get; set; }

		public UnitStats Clone()
			=> new()
			{
				BaseSpeed = BaseSpeed,
				BaseStrength = BaseStrength,
				BaseArmourMultiplier = BaseArmourMultiplier,
				DefencePierceRate = DefencePierceRate,
				UnitMaxHp = UnitMaxHp,
				DefenseModifier = DefenseModifier,
				AttackModifier = AttackModifier
			};
	}
}
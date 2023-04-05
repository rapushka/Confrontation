using System;
using UnityEngine;

namespace Confrontation
{
	public class SquadHealth
	{
		private readonly Garrison _unit;

		public SquadHealth(Garrison garrison1) => _unit = garrison1;

		public float TakeDamageOnDefence(float incomeDamage)
			=> TakeDamage(incomeDamage.ReduceBy(_unit.DefenceModifier));

		public float TakeDamage(float incomingDamage)
		{
			var isDamageLethal = IsDamageLethal(incomingDamage, out var overkillDamage);
			var remainedUnits = CalculateRemainedUnits(incomingDamage);

			_unit.QuantityOfUnits = isDamageLethal ? 0 : remainedUnits;
			return overkillDamage;
		}

		public bool IsDamageLethalOnDefence(float incomingDamage, out float overkillDamage)
			=> IsDamageLethal(incomingDamage.ReduceBy(_unit.DefenceModifier), out overkillDamage);

		public bool IsDamageLethalOnDefence(float incomingDamage)
			=> IsDamageLethal(incomingDamage.ReduceBy(_unit.DefenceModifier), out var _);

		private bool IsDamageLethal(float incomingDamage, out float overkillDamage)
		{
			var remainedUnits = CalculateRemainedUnits(incomingDamage);
			var isDamageLethal = remainedUnits <= 0;
			overkillDamage = isDamageLethal ? Mathf.Abs(remainedUnits) : 0;
			return isDamageLethal;
		}

		private int CalculateRemainedUnits(float incomingDamage) 
			=> _unit.QuantityOfUnits - ToQuantity((incomingDamage - _unit.BaseStrength).Clamp(min: 0));

		private int ToQuantity(float incomingDamage)
			=> _unit.Stats.ConvertDamageToUnitsQuantity switch
			{
				FloatToIntStrategy.Round => Mathf.RoundToInt(incomingDamage),
				FloatToIntStrategy.Floor => Mathf.FloorToInt(incomingDamage),
				FloatToIntStrategy.Ceil  => Mathf.CeilToInt(incomingDamage),
				var _                    => throw new ArgumentOutOfRangeException(),
			};
	}
}
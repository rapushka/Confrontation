using UnityEngine;

namespace Confrontation
{
	public class SquadHealth
	{
		private readonly Garrison _unit;

		private float _frontUnitCurrentHp;
		private float _frontUnitCurrentHpCandidate;
		private int _quantityOfUnitsCandidate;

		public SquadHealth(Garrison unit)
		{
			_unit = unit;
			_frontUnitCurrentHp = UnitMaxHp;
		}

		private float UnitMaxHp => _unit.Stats.UnitMaxHp;

		private bool IsCandidateDead => _quantityOfUnitsCandidate <= 0;

		private int OverkillDamage => IsCandidateDead ? Mathf.Abs(_quantityOfUnitsCandidate) : 0;

		public float TakeDamageOnDefence(float incomeDamage)
			=> TakeDamage(incomeDamage.ReduceBy(_unit.DefenceModifier));

		public float TakeDamage(float incomingDamage)
		{
			CalculateCandidates(incomingDamage);
			ApplyCandidates();
			return OverkillDamage;
		}

		public bool IsDamageLethalOnDefence(float incomingDamage)
			=> IsDamageLethalOnDefence(incomingDamage, out var _);

		public bool IsDamageLethalOnDefence(float incomingDamage, out float overkillDamage)
			=> IsDamageLethal(incomingDamage.ReduceBy(_unit.DefenceModifier), out overkillDamage);

		private bool IsDamageLethal(float incomingDamage, out float overkillDamage)
		{
			CalculateCandidates(incomingDamage);
			overkillDamage = OverkillDamage;
			return IsCandidateDead;
		}

		private void CalculateCandidates(float incomingDamage)
		{
			CacheToCandidates();

			var remainingDamage = ApplyBaseArmorFor(incomingDamage);
			InflictDamageToFrontUnit(ref remainingDamage);

			if (remainingDamage > 0)
			{
				FrontUnitDead();
				KillUnits(ref remainingDamage);
				InflictDamageToFrontUnit(ref remainingDamage);
			}
		}

		private float ApplyBaseArmorFor(float incomingDamage) => (incomingDamage - _unit.BaseArmor).Clamp(min: 0);

		private void InflictDamageToFrontUnit(ref float remainingDamage)
		{
			var inflictedDamage = remainingDamage.Clamp(max: _frontUnitCurrentHpCandidate);
			_frontUnitCurrentHpCandidate -= inflictedDamage;
			remainingDamage -= inflictedDamage;
		}

		private void KillUnits(ref float remainingDamage)
		{
			var killedUnits = Mathf.FloorToInt(remainingDamage / UnitMaxHp);
			_quantityOfUnitsCandidate -= killedUnits;
			remainingDamage -= killedUnits;
		}

		private void FrontUnitDead()
		{
			_quantityOfUnitsCandidate--;
			_frontUnitCurrentHpCandidate = UnitMaxHp;
		}

		private void ApplyCandidates()
		{
			_unit.QuantityOfUnits = _quantityOfUnitsCandidate;
			_frontUnitCurrentHp = _frontUnitCurrentHpCandidate;
		}

		private void CacheToCandidates()
		{
			_quantityOfUnitsCandidate = _unit.QuantityOfUnits;
			_frontUnitCurrentHpCandidate = _frontUnitCurrentHp;
		}
	}
}
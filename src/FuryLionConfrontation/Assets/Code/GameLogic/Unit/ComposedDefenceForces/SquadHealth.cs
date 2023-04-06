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

		private bool IsFrontUnitAlive => _frontUnitCurrentHp > 0;

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

			var remainingDamage = ApplyBaseArmor(incomingDamage);

			var inflictedDamage = remainingDamage.Clamp(max: _frontUnitCurrentHpCandidate);
			_frontUnitCurrentHpCandidate -= inflictedDamage;
			remainingDamage -= inflictedDamage;

			if (IsFrontUnitAlive)
			{
				return;
			}

			_frontUnitCurrentHpCandidate = UnitMaxHp;

			var killedUnits = Mathf.FloorToInt(remainingDamage / UnitMaxHp);
			_quantityOfUnitsCandidate -= killedUnits;
			remainingDamage -= killedUnits;

			if (_quantityOfUnitsCandidate > 0
			    && remainingDamage > 0)
			{
				_frontUnitCurrentHpCandidate = UnitMaxHp - remainingDamage;
			}
		}

		private float ApplyBaseArmor(float incomingDamage) => (incomingDamage - _unit.BaseStrength).Clamp(min: 0);

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
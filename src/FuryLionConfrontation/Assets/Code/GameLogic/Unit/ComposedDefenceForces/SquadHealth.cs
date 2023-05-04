using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class SquadHealth : IInitializable
	{
		[Inject] private readonly Garrison _unit;

		private float _frontUnitCurrentHp;
		private float _frontUnitCurrentHpCandidate;
		private int _quantityOfUnitsCandidate;

		public void Initialize() => _frontUnitCurrentHp = UnitMaxHp;

		public float HealthPoints => _unit.QuantityOfUnits * _unit.Stats.UnitMaxHp + _frontUnitCurrentHp;

		private float UnitMaxHp => _unit.Stats.UnitMaxHp;

		private bool IsCandidateDead => _quantityOfUnitsCandidate <= 0;

		private int OverkillDamage => IsCandidateDead ? Mathf.Abs(_quantityOfUnitsCandidate) : 0;

		public float TakeDamageOnDefence(float incomeDamage, float pierceRate = 0f)
			=> TakeDamage(incomeDamage.ReduceBy(Pierce(pierceRate)));

		public float TakeDamage(float incomingDamage)
		{
			CalculateCandidates(incomingDamage);
			ApplyCandidates();
			return OverkillDamage;
		}

		public bool IsDamageLethalOnDefence(float incomingDamage, float pierceRate = 0f)
			=> IsDamageLethalOnDefence(incomingDamage, out var _, pierceRate);

		public bool IsDamageLethalOnDefence(float incomingDamage, out float overkillDamage, float pierceRate = 0f)
			=> IsDamageLethal(incomingDamage.ReduceBy(Pierce(pierceRate)), out overkillDamage);

		private float Pierce(float pierceRate) => _unit.DefenceModifier - pierceRate;

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

		private float ApplyBaseArmorFor(float damage) => damage.ReduceBy(_unit.BaseArmor).Clamp(min: 0);

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
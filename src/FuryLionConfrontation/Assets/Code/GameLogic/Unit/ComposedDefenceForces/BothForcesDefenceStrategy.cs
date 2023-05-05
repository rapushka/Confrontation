using Zenject;

namespace Confrontation
{
	public class BothForcesDefenceStrategy : IDefenceStrategy
	{
		[Inject] private readonly UnitsSquad _locatedSquad;
		[Inject] private readonly Garrison _garrison;
		[Inject] private readonly Cell _cell;
		[Inject] private readonly IDestroyService _destroyService;

		private float _pierceRate;

		public float BaseDamage => _locatedSquad.BaseDamage + _garrison.BaseDamage;

		public int QuantityOfUnits => _locatedSquad.QuantityOfUnits + _garrison.QuantityOfUnits;

		public float HealthPoints => _locatedSquad.HealthPoints + _garrison.HealthPoints;

		public void Destroy()
		{
			_destroyService.Destroy(_locatedSquad.gameObject);
			_destroyService.Destroy(_garrison.gameObject);
		}

		public void Kill()
		{
			_locatedSquad.Kill();
			_garrison.Kill();
		}

		public void TakeDamageOnDefence(float incomingDamage, float pierceRate)
		{
			_pierceRate = pierceRate;
			if (TryKillBoth(incomingDamage) == false
			    && TryTakeAllDamageEqually(incomingDamage) == false)
			{
				DistributeDamage(incomingDamage);
			}
		}

		private bool TryKillBoth(float incomingDamage)
		{
			var isLethalForGarrison
				= _garrison.Health.IsDamageLethalOnDefence(incomingDamage, out var overkillDamage, _pierceRate);
			var isLethalForLocatedSquad = _locatedSquad.Health.IsDamageLethalOnDefence(overkillDamage, _pierceRate);

			if (isLethalForGarrison && isLethalForLocatedSquad)
			{
				// Damage to both is lethal anyway, so there's no difference
				_locatedSquad.Health.TakeDamageOnDefence(incomingDamage, _pierceRate);
				_garrison.Health.TakeDamageOnDefence(incomingDamage, _pierceRate);
				return true;
			}

			return false;
		}

		private bool TryTakeAllDamageEqually(float damage)
		{
			var damageForUnits = damage / 2;
			var damageForGarrison = damage - damageForUnits;

			if (_locatedSquad.Health.IsDamageLethalOnDefence(damageForUnits, _pierceRate) == false
			    && _garrison.Health.IsDamageLethalOnDefence(damageForGarrison, _pierceRate) == false)
			{
				_locatedSquad.Health.TakeDamageOnDefence(damageForUnits, _pierceRate);
				_garrison.Health.TakeDamageOnDefence(damageForGarrison, _pierceRate);
				return true;
			}

			return false;
		}

		private void DistributeDamage(float damage)
		{
			if (_locatedSquad.QuantityOfUnits.IncreaseBy(_locatedSquad.DefenceModifier)
			    > _garrison.QuantityOfUnits.IncreaseBy(_garrison.DefenceModifier))
			{
				KillGarrison(damage);
			}
			else
			{
				KillLocatedSquad(damage);
			}
		}

		private void KillGarrison(float damage)
			=> DistributeTo(damage, fullDamaged: _garrison, partiallyDamaged: _locatedSquad);

		private void KillLocatedSquad(float damage)
		{
			DistributeTo(damage, fullDamaged: _locatedSquad, partiallyDamaged: _garrison);
			_cell.MakeRegionNeutral();
		}

		private void DistributeTo(float incomingDamage, Garrison fullDamaged, Garrison partiallyDamaged)
		{
			var remainedDamage = fullDamaged.Health.TakeDamageOnDefence(incomingDamage, _pierceRate);
			_destroyService.Destroy(fullDamaged.gameObject);
			partiallyDamaged.Health.TakeDamageOnDefence(remainedDamage, _pierceRate);
		}

		public class Factory : PlaceholderFactory<UnitsSquad, Garrison, Cell, BothForcesDefenceStrategy> { }
	}
}
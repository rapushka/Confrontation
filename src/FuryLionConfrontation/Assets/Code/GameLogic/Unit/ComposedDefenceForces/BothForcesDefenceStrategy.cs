namespace Confrontation
{
	public class BothForcesDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _locatedSquad;
		private readonly Garrison _garrison;
		private readonly Cell _cell;

		public BothForcesDefenceStrategy(IDestroyer destroyer, Cell cell, Garrison locatedSquad, Garrison garrison)
			: base(destroyer)
		{
			_cell = cell;
			_locatedSquad = locatedSquad;
			_garrison = garrison;
		}

		public override float BaseDamage => _locatedSquad.BaseStrength + _garrison.BaseStrength;

		public override int QuantityOfUnits
		{
			get => _locatedSquad.QuantityOfUnits + _garrison.QuantityOfUnits;
			set
			{
				var half = value / 2;
				_locatedSquad.QuantityOfUnits = half;
				_garrison.QuantityOfUnits = value - half;
			}
		}

		public override void Destroy()
		{
			Destroyer.Destroy(_locatedSquad.gameObject);
			Destroyer.Destroy(_garrison.gameObject);
		}

		public override void TakeDamageOnDefence(float incomingDamage)
		{
			if (TryKillBoth(incomingDamage) == false
			    && TryTakeAllDamageEqually(incomingDamage) == false)
			{
				DistributeDamage(incomingDamage);
			}
		}

		private bool TryKillBoth(float incomingDamage)
		{
			var isLethalForGarrison = _garrison.Health.IsDamageLethalOnDefence(incomingDamage, out var overkillDamage);
			var isLethalForLocatedSquad = _locatedSquad.Health.IsDamageLethalOnDefence(overkillDamage);

			if (isLethalForGarrison && isLethalForLocatedSquad)
			{
				// Damage to both is lethal anyway, so there's no difference
				_locatedSquad.Health.TakeDamageOnDefence(incomingDamage);
				_garrison.Health.TakeDamageOnDefence(incomingDamage);
				return true;
			}

			return false;
		}

		private bool TryTakeAllDamageEqually(float damage)
		{
			var damageForUnits = damage / 2;
			var damageForGarrison = damage - damageForUnits;

			if (_locatedSquad.Health.IsDamageLethalOnDefence(damageForUnits) == false
			    && _garrison.Health.IsDamageLethalOnDefence(damageForGarrison) == false)
			{
				_locatedSquad.Health.TakeDamageOnDefence(damageForUnits);
				_garrison.Health.TakeDamageOnDefence(damageForGarrison);
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
			var remainedDamage = fullDamaged.Health.TakeDamageOnDefence(incomingDamage);
			Destroyer.Destroy(fullDamaged.gameObject);
			partiallyDamaged.Health.TakeDamageOnDefence(remainedDamage);
		}
	}
}
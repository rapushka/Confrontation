namespace Confrontation
{
	public class BothForcesDefenceStrategy : DefenceStrategyBase
	{
		private readonly Garrison _locatedSquad;
		private readonly Garrison _garrison;
		private readonly Cell _cell;

		public BothForcesDefenceStrategy(IAssetsService assets, Cell cell, Garrison locatedSquad, Garrison garrison)
			: base(assets)
		{
			_cell = cell;
			_locatedSquad = locatedSquad;
			_garrison = garrison;
		}

		public override float BaseDamage => _locatedSquad.BaseDamage + _garrison.BaseDamage;

		public override int QuantityOfUnits => _locatedSquad.QuantityOfUnits + _garrison.QuantityOfUnits;

		private float DefenceModifier => (_locatedSquad.DefenceModifier + _garrison.DefenceModifier) / 2;

		public override void Destroy()
		{
			Assets.Destroy(_locatedSquad.gameObject);
			Assets.Destroy(_garrison.gameObject);
		}

		public override void TakeDamageOnDefence(float damage)
		{
			if (QuantityOfUnits <= damage * DefenceModifier)
			{
				_locatedSquad.TakeDamageOnDefence(damage);
				_garrison.TakeDamageOnDefence(damage);
				return;
			}

			if (TryTakeAllDamageEqually(damage) == false)
			{
				DistributeDamage(damage);
			}
		}

		private bool TryTakeAllDamageEqually(float damage)
		{
			var damageForUnits = damage / 2;
			var damageForGarrison = damage - damageForUnits;

			if (_locatedSquad.QuantityOfUnits <= damageForUnits
			    || _garrison.QuantityOfUnits <= damageForGarrison)
			{
				return false;
			}

			_locatedSquad.TakeDamageOnDefence(damageForUnits);
			_garrison.TakeDamageOnDefence(damageForGarrison);
			return true;
		}

		private void DistributeDamage(float damage)
		{
			if (_locatedSquad.QuantityOfUnits > _garrison.QuantityOfUnits)
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

		private void DistributeTo(float incomeDamage, Garrison fullDamaged, Garrison partiallyDamaged)
		{
			var remainedDamage = incomeDamage - fullDamaged.QuantityOfUnits.ReduceBy(fullDamaged.DefenceModifier);
			Assets.Destroy(fullDamaged.gameObject);
			partiallyDamaged.TakeDamageOnDefence(remainedDamage);
		}
	}
}
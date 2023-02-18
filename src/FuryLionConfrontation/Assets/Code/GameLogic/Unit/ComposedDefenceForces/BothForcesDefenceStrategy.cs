using System;

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

		public override int Quantity => _locatedSquad.QuantityOfUnits + _garrison.QuantityOfUnits;

		public override void Destroy()
		{
			Assets.Destroy(_locatedSquad.gameObject);
			Assets.Destroy(_garrison.gameObject);
		}

		public override void TakeDamage(int damage)
		{
			if (IsBothForcesEnough(damage) == false)
			{
				DistributeDamage(damage);
			}
		}

		private bool IsBothForcesEnough(int damage)
		{
			var damageForUnits = damage / 2;
			var damageForGarrison = damage - damageForUnits;

			if (_locatedSquad.QuantityOfUnits <= damageForUnits
			    || _garrison.QuantityOfUnits <= damageForGarrison)
			{
				return false;
			}

			_locatedSquad.QuantityOfUnits -= damageForUnits;
			_garrison.QuantityOfUnits -= damageForGarrison;
			return true;
		}

		private void DistributeDamage(int damage)
		{
			if (_locatedSquad.QuantityOfUnits > _garrison.QuantityOfUnits)
			{
				DistributeTo(damage, fullDamaged: _garrison, partiallyDamaged: _locatedSquad);
			}
			else if (_locatedSquad.QuantityOfUnits < _garrison.QuantityOfUnits)
			{
				DistributeTo(damage, fullDamaged: _locatedSquad, partiallyDamaged: _garrison);
				_cell.MakeRegionNeutral();
			}
			else
			{
				throw new InvalidOperationException(WrongStrategyException);
			}
		}

		private void DistributeTo(int incomeDamage, Garrison fullDamaged, Garrison partiallyDamaged)
		{
			var remainedDamage = incomeDamage - fullDamaged.QuantityOfUnits;
			Assets.Destroy(fullDamaged.gameObject);
			partiallyDamaged.QuantityOfUnits -= remainedDamage;
		}
	}
}
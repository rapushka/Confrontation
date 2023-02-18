using System;

namespace Confrontation
{
	public class BothForcesStrategy : DefenceStrategyBase
	{
		private readonly Garrison _units;
		private readonly Garrison _garrison;
		private readonly Cell _cell;

		public BothForcesStrategy
		(
			IAssetsService assets,
			Cell cell,
			Garrison units,
			Garrison garrison
		)
		:base (assets)
		{
			_cell = cell;
			_units = units;
			_garrison = garrison;
		}

		public override int Quantity => _units.QuantityOfUnits + _garrison.QuantityOfUnits;

		public override void Destroy()
		{
			Assets.Destroy(_units.gameObject);
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

			if (_units.QuantityOfUnits <= damageForUnits
			    || _garrison.QuantityOfUnits <= damageForGarrison)
			{
				return false;
			}

			_units.QuantityOfUnits -= damageForUnits;
			_garrison.QuantityOfUnits -= damageForGarrison;
			return true;
		}

		private void DistributeDamage(int incomeDamage)
		{
			if (_units.QuantityOfUnits > _garrison.QuantityOfUnits)
			{
				Change(incomeDamage, destroyed: _garrison, remained: _units);
			}
			else if (_units.QuantityOfUnits < _garrison.QuantityOfUnits)
			{
				Change(incomeDamage, destroyed: _units, remained: _garrison);
				_cell.MakeRegionNeutral();
			}
			else
			{
				throw new InvalidOperationException("Whole quantity is less than damage => Defenders lost");
			}
		}

		private void Change(int incomeDamage, Garrison destroyed, Garrison remained)
		{
			var remainedDamage = incomeDamage - destroyed.QuantityOfUnits;
			remained.QuantityOfUnits -= remainedDamage;
			Assets.Destroy(destroyed.gameObject);
		}
	}
}
using System;
using Zenject;

namespace Confrontation
{
	public class DefenceStrategyForCellFactory : IFactory<Cell, IDefenceStrategy>
	{
		[Inject] private readonly BothForcesDefenceStrategy.Factory _bothForcesFactory;
		[Inject] private readonly SingleForceDefenceStrategy.Factory _singleForceFactory;

		public IDefenceStrategy Create(Cell cell)
		{
			var units = cell.LocatedUnits;
			var garrison = cell.Garrison;
			var isThereUnits = units == true;
			var isThereGarrison = garrison == true;
			var isThereBoth = isThereUnits && isThereGarrison;

			return isThereBoth    ? _bothForcesFactory.Create(units, garrison, cell)
				: isThereUnits    ? _singleForceFactory.Create(units)
				: isThereGarrison ? _singleForceFactory.Create(garrison)
				                    : throw new InvalidOperationException(Constants.Exception.ThereIsNoDefenders);
		}
	}
}
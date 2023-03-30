using System;

namespace Confrontation
{
	public abstract class DefenceStrategyBase : IDefenceStrategy
	{
		private const string ThereIsNoDefendersException
			= "Defence strategy can't be picked for cell without defence forces";

		protected const string WrongStrategyException
			= "Whole quantity is less than Damage => Defenders lost and wrong strategy was picked";

		protected DefenceStrategyBase(IAssetsService assets) => Assets = assets;

		public abstract int DefenceStrength { get; }

		protected IAssetsService Assets { get; }

		public abstract void Destroy();

		public abstract void TakeDamage(int damage);

		public static IDefenceStrategy Create(IAssetsService assets, Cell cell)
		{
			var units = cell.LocatedUnits;
			var garrison = cell.Garrison;
			var isThereUnits = units == true;
			var isThereGarrison = garrison == true;
			var isThereBoth = isThereUnits && isThereGarrison;

			return isThereBoth    ? new BothForcesDefenceStrategy(assets, cell, units, garrison)
				: isThereUnits    ? new SingleForceDefenceStrategy(assets, units)
				: isThereGarrison ? new SingleForceDefenceStrategy(assets, garrison)
				                    : throw new InvalidOperationException(ThereIsNoDefendersException);
		}
	}
}
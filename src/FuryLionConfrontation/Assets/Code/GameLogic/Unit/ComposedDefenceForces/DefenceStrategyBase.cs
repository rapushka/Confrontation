using System;

namespace Confrontation
{
	public abstract class DefenceStrategyBase : IDefenceStrategy
	{
		private const string ThereIsNoDefendersException
			= "Defence strategy can't be picked for cell without defence forces";

		protected const string WrongStrategyException
			= "Whole quantity is less than Damage => Defenders lost and wrong strategy was picked";

		protected DefenceStrategyBase(IDestroyer destroyer) => Destroyer = destroyer;

		public abstract float BaseDamage { get; }

		public abstract int QuantityOfUnits { get; }

		public abstract float HealthPoints { get; }

		protected IDestroyer Destroyer { get; }

		public abstract void Destroy();

		public abstract void Kill();

		public abstract void TakeDamageOnDefence(float incomingDamage, float pierceRate);

		public static IDefenceStrategy Create(IDestroyer destroyer, Cell cell)
		{
			var units = cell.LocatedUnits;
			var garrison = cell.Garrison;
			var isThereUnits = units == true;
			var isThereGarrison = garrison == true;
			var isThereBoth = isThereUnits && isThereGarrison;

			return isThereBoth    ? new BothForcesDefenceStrategy(destroyer, cell, units, garrison)
				: isThereUnits    ? new SingleForceDefenceStrategy(destroyer, units)
				: isThereGarrison ? new SingleForceDefenceStrategy(destroyer, garrison)
				                    : throw new InvalidOperationException(ThereIsNoDefendersException);
		}
	}
}
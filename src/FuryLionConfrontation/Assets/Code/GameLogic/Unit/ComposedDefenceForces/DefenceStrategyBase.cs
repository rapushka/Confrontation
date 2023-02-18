using System;

namespace Confrontation
{
	public abstract class DefenceStrategyBase : IDefenceStrategy
	{
		protected readonly IAssetsService Assets;

		protected DefenceStrategyBase(IAssetsService assets)
		{
			Assets = assets;
		}

		public abstract int  Quantity { get; }
		public abstract void Destroy();
		public abstract void TakeDamage(int damage);

		public static IDefenceStrategy Create(IAssetsService assets, Cell cell)
		{
			var units = cell.LocatedUnits;
			var garrison = cell.Garrison;
			var isThereUnits = units == true;
			var isThereGarrison = garrison == true;

			return isThereUnits && isThereGarrison ? new BothForcesStrategy(assets, cell, units, garrison)
				: isThereUnits                     ? new OnlyOneForceDefenceStrategy(assets, units)
				: isThereGarrison                  ? new OnlyOneForceDefenceStrategy(assets, garrison)
				                                     : Exception();
		}

		private static IDefenceStrategy Exception()
			=> throw new InvalidOperationException("Defence strategy can't be for cell without defence forces");
	}
}
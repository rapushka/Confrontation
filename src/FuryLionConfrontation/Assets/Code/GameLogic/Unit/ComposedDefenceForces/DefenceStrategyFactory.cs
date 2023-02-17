using System;

namespace Confrontation
{
	public class DefenceStrategyFactory
	{
		public static IDefenceStrategy Create(IFlexDefenceStrategy flexDefence, Garrison units, Garrison garrison)
		{
			var isThereUnits = units == true;
			var isThereGarrison = garrison == true;

			return isThereUnits && isThereGarrison ? new BothForcesStrategy(flexDefence, units, garrison)
				: isThereUnits                     ? new UnitsDefenceStrategy(units)
				: isThereGarrison                  ? new GarrisonDefenceStrategy(garrison)
				                                     : Exception();
		}

		private static GarrisonDefenceStrategy Exception()
			=> throw new Exception("Defence strategy can't be for cell without defence forces");
	}
}
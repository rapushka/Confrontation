using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Spawn
	{
		public static UnitsSquad Units(IEnumerable<Building> buildings, Func<Barrack, bool> that, int quantity = 1)
		{
			var barracks = buildings.OfType<Barrack>().First(that);
			return barracks.SpawnUnits(quantity);
		}
	}

	public static class SpawnExtensions
	{
		public static UnitsSquad SpawnUnit(this Barrack @this) => @this.SpawnUnits(quantity: 1);

		public static UnitsSquad SpawnUnits(this Barrack @this, int quantity)
		{
			for (var i = 0; i < quantity; i++)
			{
				@this.Action();
			}

			return @this.RelatedCell.LocatedUnits!;
		}
	}
}
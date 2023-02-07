using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Spawn
	{
		public static UnitsSquad Units(IEnumerable<Building> buildings, Func<Barracks, bool> that, int quantity = 1)
		{
			var barracks = buildings.OfType<Barracks>().First(that);
			return barracks.SpawnUnits(quantity);
		}
	}

	public static class SpawnExtensions
	{
		public static UnitsSquad SpawnUnit(this Barracks @this) => @this.SpawnUnits(quantity: 1);

		public static UnitsSquad SpawnUnits(this Barracks @this, int quantity)
		{
			for (var i = 0; i < quantity; i++)
			{
				@this.Action();
			}

			return @this.RelatedCell.LocatedUnits!;
		}
	}
}
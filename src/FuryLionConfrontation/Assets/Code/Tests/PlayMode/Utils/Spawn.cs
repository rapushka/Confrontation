using System;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Spawn
	{
		public static UnitsSquad Units(IEnumerable<Building> buildings, Func<Barracks, bool> predicate, int quantity = 1)
		{
			var barracks = buildings.OfType<Barracks>().First(predicate);
			for (var i = 0; i < quantity; i++)
			{
				barracks.Action();
			}
			return barracks.RelatedCell.UnitsSquads!;
		}
	}
}
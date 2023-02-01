using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor.PlayModeTests
{
	public static class Spawn
	{
		public static UnitsSquad Units(IEnumerable<Building> buildings, Func<Barracks, bool> predicate)
		{
			var barracks = buildings.OfType<Barracks>().First(predicate);
			barracks.Action();
			return barracks.RelatedCell.UnitsSquads!;
		}
	}
}
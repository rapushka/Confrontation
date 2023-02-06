using System;
using System.Collections.Generic;

namespace Confrontation
{
	[Serializable]
	public class RegionData
	{
		public int               OwnerPlayerId      { get; set; }
		public Coordinates       VillageCoordinates { get; set; }
		public List<Coordinates> CellsCoordinates   { get; set; } = new();
	}
}
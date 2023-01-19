using System;

namespace Confrontation
{
	[Serializable]
	public class Level
	{
		public Cell.Data[,] Cells   { get; set; }
		public Player.Data[]     Players { get; set; }
	}
}
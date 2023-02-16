using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class Village : Building
	{
		public override string Name => nameof(Village);

		public IEnumerable<Cell> CellsInRegion
		{
			get
			{
				var region = Field.Regions[Coordinates];

				var regionsCoordinates = Field.Regions.Where((r) => r == region)
				                              .Select((r) => r.Coordinates);

				foreach (var coordinates in regionsCoordinates)
				{
					yield return Field.Cells[coordinates];
				}
			}
		}
	}
}
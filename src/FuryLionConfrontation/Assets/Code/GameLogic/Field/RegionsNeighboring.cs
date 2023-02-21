using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Confrontation
{
	public class RegionsNeighboring : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize()
		{
			var dictionary = _field.Regions.ToDictionary((r) => r, GroupByNeighbouring);
		}

		private IEnumerable<Region> GroupByNeighbouring(Region region)
		{
			var neighbourRegions = new List<Region>();
			var neighborsForCell = region.CellsInRegion.ToDictionary(c => c, CollectNeighboursFor);

			foreach (var (_, regions) in neighborsForCell)
			{
				var newRegions = regions.Where((r) => neighbourRegions.Contains(r) == false);
				neighbourRegions.AddRange(newRegions);
			}

			return neighbourRegions;
		}

		private IEnumerable<Region> CollectNeighboursFor(Cell cell)
			=> CollectNeighbors(cell, cell.Coordinates.Row.IsEven() ? IsDiagonallyNext : IsDiagonallyPrevious);

		private IEnumerable<Region> CollectNeighbors(Cell cell, Func<Coordinates, Coordinates, bool> isNeighbor)
		{
			var centerRow = cell.Coordinates.Row;
			var centerColumn = cell.Coordinates.Column;

			for (var row = centerRow - 1; row < centerRow + 1; row++)
			{
				for (var column = centerColumn - 1; column < centerColumn + 1; column++)
				{
					var currentCoordinates = new Coordinates(row, column);
					
					if (_field.Cells.Sizes.IsInBounds(row, column)
					    && isNeighbor(cell.Coordinates, currentCoordinates) == false)
					{
						yield return cell.RelatedRegion;
					}
				}
			}
		}

		private static bool IsDiagonallyNext(Coordinates center, Coordinates other)
			=> center.Column + 1 == other.Column
			   && center.Row != other.Row;

		private static bool IsDiagonallyPrevious(Coordinates center, Coordinates other)
			=> center.Column - 1 == other.Column
			   && center.Row != other.Row;
	}
}
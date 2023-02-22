using System;
using Zenject;

namespace Confrontation
{
	public class RegionsNeighboringCalculator : IInitializable
	{
		[Inject] private readonly IField _field;

		public void Initialize()
		{
			foreach (var region in _field.Regions.OnlyUnique())
			{
				foreach (var cell in region.CellsInRegion)
				{
					CollectNeighboursFor(cell, region);
				}
			}
		}

		private void CollectNeighboursFor(ICoordinated cell, Region region)
			=> ForEachCell(around: cell.Coordinates, @do: (c) => AddNeighbour(region, c));

		private void AddNeighbour(Region region, Cell currentCell)
			=> _field.Neighboring.AddNeighboring(region, currentCell.RelatedRegion);

		private void ForEachCell(Coordinates around, Action<Cell> @do)
		{
			Func<Coordinates, Coordinates, bool> isFarEnough = around.Row.IsEven()
				? IsDiagonallyNext
				: IsDiagonallyPrevious;

			for (var row = around.Row - 1; row <= around.Row + 1; row++)
			{
				for (var column = around.Column - 1; column <= around.Column + 1; column++)
				{
					var currentCoordinates = new Coordinates(row, column);

					if (_field.Cells.Sizes.IsInBounds(currentCoordinates)
					    && isFarEnough(around, currentCoordinates) == false)
					{
						var currentCell = _field.Cells[currentCoordinates];

						@do(currentCell);
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
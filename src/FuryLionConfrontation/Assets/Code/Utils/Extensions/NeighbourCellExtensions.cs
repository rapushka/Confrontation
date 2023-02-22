using System;

namespace Confrontation
{
	public static class NeighbourCellExtensions
	{
		public static void ForEachCellAround(this ICoordinated @this, Action<Cell> @do, CoordinatedMatrix<Cell> on)
		{
			Func<Coordinates, Coordinates, bool> isFarEnough = @this.Coordinates.Row.IsEven()
				? IsDiagonallyNext
				: IsDiagonallyPrevious;

			for (var row = @this.Coordinates.Row - 1; row <= @this.Coordinates.Row + 1; row++)
			{
				for (var column = @this.Coordinates.Column - 1; column <= @this.Coordinates.Column + 1; column++)
				{
					var currentCoordinates = new Coordinates(row, column);

					if (on.Sizes.IsInBounds(currentCoordinates)
					    && isFarEnough(@this.Coordinates, currentCoordinates) == false)
					{
						@do(on[currentCoordinates]);
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
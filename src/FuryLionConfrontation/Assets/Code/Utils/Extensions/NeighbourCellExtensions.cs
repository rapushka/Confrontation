using System;

namespace Confrontation
{
	public static class NeighbourCellExtensions
	{
		public static void ForEachCellAround(this Coordinates @this, Action<Cell> @do, CoordinatedMatrix<Cell> on)
		{
			Func<Coordinates, Coordinates, bool> isFarEnough = @this.Row.IsEven()
				? IsDiagonallyNext
				: IsDiagonallyPrevious;

			for (var row = @this.Row - 1; row <= @this.Row + 1; row++)
			{
				for (var column = @this.Column - 1; column <= @this.Column + 1; column++)
				{
					var currentCoordinates = new Coordinates(row, column);

					if (on.Sizes.IsInBounds(currentCoordinates)
					    && isFarEnough(@this, currentCoordinates) == false)
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Confrontation
{
	public class CoordinatedMatrix<T> : IEnumerable<T>
		where T : ICoordinated
	{
		private readonly T[,] _matrix;

		public CoordinatedMatrix(Coordinates coordinates) => _matrix = new T[coordinates.Row, coordinates.Column];

		public CoordinatedMatrix(Sizes sizes) => _matrix = new T[sizes.Height, sizes.Width];
		public CoordinatedMatrix(int height, int width) => _matrix = new T[height, width];

		public T this[Coordinates coordinates]
		{
			get => _matrix[coordinates.Row, coordinates.Column];
			set => _matrix[coordinates.Row, coordinates.Column] = value;
		}

		public T this[Sizes sizes]
		{
			get => _matrix[sizes.Height, sizes.Width];
			set => _matrix[sizes.Height, sizes.Width] = value;
		}

		public void SetForEach(Func<int, int, T> action)
		{
			for (var i = 0; i < _matrix.GetLength(0); i++)
			{
				for (var j = 0; j < _matrix.GetLength(1); j++)
				{
					_matrix[i, j] = action.Invoke(i, j);
				}
			}
		}

		public IEnumerator<T> GetEnumerator() => _matrix.Cast<T>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _matrix.GetEnumerator();
	}
}
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

		public CoordinatedMatrix(Coordinates coordinates) : this(new Sizes(coordinates.Row, coordinates.Column)) { }

		public CoordinatedMatrix(int height, int width) : this(new Sizes(height, width)) { }

		public CoordinatedMatrix(Sizes sizes)
		{
			Sizes = sizes;
			_matrix = new T[sizes.Height, sizes.Width];
		}

		public Sizes Sizes { get; }

		public T this[Coordinates coordinates]
		{
			get => _matrix[coordinates.Row, coordinates.Column];
			set => _matrix[coordinates.Row, coordinates.Column] = value;
		}
		
		public T this[int row, int column]
		{
			get => _matrix[row, column];
			set => _matrix[row, column] = value;
		}

		public T this[Sizes sizes]
		{
			get => _matrix[sizes.Height, sizes.Width];
			set => _matrix[sizes.Height, sizes.Width] = value;
		}

		public void Add(T item) => this[item.Coordinates] = item;

		public void Remove(T item) => this[item.Coordinates] = default;

		public void DoubleFor(Action<int, int> action)
		{
			for (var i = 0; i < _matrix.GetLength(0); i++)
			{
				for (var j = 0; j < _matrix.GetLength(1); j++)
				{
					action.Invoke(i, j);
				}
			}
		}

		public IEnumerator<T> GetEnumerator() => _matrix.Cast<T>().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _matrix.GetEnumerator();
	}
}
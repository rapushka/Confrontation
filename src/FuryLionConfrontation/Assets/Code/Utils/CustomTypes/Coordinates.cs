using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Coordinates
	{
		[SerializeField] private int _row;
		[SerializeField] private int _column;

		public int Row => _row;

		public int Column => _column;

		public Coordinates(int row, int column)
		{
			_row = row;
			_column = column;
		}

		// https://www.redblobgames.com/grids/hexagons/
		public Vector2 CalculatePosition()
			=> new(Constants.HexagonWidth * Column + HorizontalOffset(Row), Row * VerticalDistance());

		public static bool operator ==(Coordinates left, Coordinates right)
			=> left.Row == right.Row && left.Column == right.Column;

		public static bool operator !=(Coordinates left, Coordinates right) => !(left == right);

		public override bool Equals(object obj) => obj is Coordinates other && Equals(other);

		public bool Equals(Coordinates other) => Row == other.Row && Column == other.Column;

		public override int GetHashCode() => HashCode.Combine(Row, Column);

		private static float HorizontalOffset(int row) => row.IsEven() ? 0f : Constants.HorizontalOffsetForOddRows;

		private static float VerticalDistance() => Constants.HexagonWidth * 3 / (2 * Mathf.Sqrt(3));

		public override string ToString() => $"{nameof(Row)}: {Row}, {nameof(Column)}: {Column}";
	}
}
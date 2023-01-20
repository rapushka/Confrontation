using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Coordinates
	{
		[field: SerializeField] public int Row { get; private set; }

		[field: SerializeField] public int Column { get; private set; }

		public Coordinates(int row, int column)
		{
			Row = row;
			Column = column;
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
	}
}
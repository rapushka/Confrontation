using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Coordinates
	{
		private const float NoOffset = 0f;

		private const float Offset = 0.5f;

		[field: SerializeField] public int Row { get; private set; }

		[field: SerializeField] public int Column { get; private set; }

		public Coordinates(int row, int column)
		{
			Row = row;
			Column = column;
		}

		// https://www.redblobgames.com/grids/hexagons/
		public Vector2 CalculatePosition() => new(Column + HorizontalOffset(Row), Row * VerticalDistance());

		public static bool operator ==(Coordinates left, Coordinates right)
			=> left.Row == right.Row && left.Column == right.Column;

		public static bool operator !=(Coordinates left, Coordinates right) => !(left == right);

		public override bool Equals(object obj) => obj is Coordinates other && Equals(other);

		public bool Equals(Coordinates other) => Row == other.Row && Column == other.Column;

		public override int GetHashCode() => HashCode.Combine(Row, Column);

		private static float HorizontalOffset(int row) => row.IsEven() ? NoOffset : Offset;

		private static float VerticalDistance() => 3 / (2 * Mathf.Sqrt(3));
	}
}
using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Coordinates
	{
		private const float NoOffset = 0f;
		private const float Offset = 0.5f;

		[field: SerializeField] public int Row    { get; private set; }
		[field: SerializeField] public int Column { get; private set; }

		public Coordinates(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public static bool operator ==(Coordinates left, Coordinates right)
			=> left.Row == right.Row && left.Column == right.Column;

		public static bool operator !=(Coordinates left, Coordinates right) => !(left == right);

		// https://www.redblobgames.com/grids/hexagons/
		public Vector2 CalculatePosition() => new(Column + HorizontalOffset(Row), Row * VerticalDistance());

		private static float HorizontalOffset(int row) => row.IsEven() ? NoOffset : Offset;

		private static float VerticalDistance() => 3 / (2 * Mathf.Sqrt(3));
	}
}
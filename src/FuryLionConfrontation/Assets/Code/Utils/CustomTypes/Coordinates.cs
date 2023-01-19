using System;
using UnityEngine;

namespace Confrontation
{
	[Serializable]
	public struct Coordinates
	{
		private const float NoOffset = 0f;
		private const float Offset = 0.5f;

		[SerializeField] private Vector2Int _vector;

		public Coordinates(int row, int column) => _vector = new Vector2Int(row, column);

		public int Row => _vector.x;

		public int Column => _vector.y;

		// https://www.redblobgames.com/grids/hexagons/
		public Vector2 CalculatePosition() => new(Column + HorizontalOffset(Row), Row * VerticalDistance());

		private static float HorizontalOffset(int row) => row.IsEven() ? NoOffset : Offset;

		private static float VerticalDistance() => 3 / (2 * Mathf.Sqrt(3));
	}
}
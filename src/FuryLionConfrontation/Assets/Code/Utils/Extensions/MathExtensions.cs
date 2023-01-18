using UnityEngine;

namespace Code
{
	public static class MathExtensions
	{
		private const float NoOffset = 0f;
		private const float Offset = 0.5f;

		// https://www.redblobgames.com/grids/hexagons/
		public static Vector3 CalculatePosition(this Vector2Int @this)
			=> new(x: @this.x + HorizontalOffset(@this.y), y: 0, z: @this.y * VerticalDistance());

		private static float HorizontalOffset(int row) => row.IsEven() ? NoOffset : Offset;

		private static float VerticalDistance() => 3 / (2 * Mathf.Sqrt(3));

		private static bool IsEven(this int number) => number % 2 == 0;
	}
}
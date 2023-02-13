using UnityEngine;

namespace Confrontation
{
	public struct Bounds
	{
		public Vector2 MinBorder;
		public Vector2 MaxBorder;

		private Vector2 Center => new((MinBorder.x + MaxBorder.x) / 2, (MinBorder.y + MaxBorder.y) / 2);

		public void UpdateBounds(Vector2 position)
		{
			MaxBorder.x = MaxAbs(MaxBorder.x, position.x);
			MaxBorder.y = MaxAbs(MaxBorder.y, position.y);

			MinBorder.x = MinAbs(MinBorder.x, position.x);
			MinBorder.y = MinAbs(MinBorder.y, position.y);
		}

		public float Distance(Vector2 position)
		{
			var clamped = position.Clamp(MaxBorder, MinBorder);
			return Vector2.Distance(position, clamped);
		}

		public bool IsInBounce(Vector2 position)
			=> position.IsGreater(than: MinBorder)
			   && position.IsLess(than: MaxBorder);

		private static float MinAbs(float a, float b) => Mathf.Abs(a) < Mathf.Abs(b) ? a : b;

		private static float MaxAbs(float a, float b) => Mathf.Abs(a) > Mathf.Abs(b) ? a : b;
	}
}
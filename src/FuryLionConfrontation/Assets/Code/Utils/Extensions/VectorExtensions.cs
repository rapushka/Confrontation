using UnityEngine;

namespace Confrontation
{
	public static class VectorExtensions
	{
		public static Vector3 AsTopDown(this Vector2 @this) => new(x: @this.x, y: 0, z: @this.y);

		public static Vector2 FromTopDown(this Vector3 @this) => new(x: @this.x, y: @this.z);

		public static Vector3 SetY(this Vector3 @this, float value)
		{
			@this.y = value;
			return @this;
		}

		public static Vector2 Clamp(this Vector2 @this, Vector2 min, Vector2 max)
			=> new
			(
				Mathf.Clamp(@this.x, min.x, max.x),
				Mathf.Clamp(@this.y, min.y, max.y)
			);
	}
}
using UnityEngine;

namespace Confrontation
{
	public static class VectorExtensions
	{
		public static Vector3 AsTopDown(this Vector2 @this) => new(x: @this.x, y: 0, z: @this.y);

		public static Vector3 SetY(this Vector3 @this, float value)
		{
			@this.y = value;
			return @this;
		}
	}
}
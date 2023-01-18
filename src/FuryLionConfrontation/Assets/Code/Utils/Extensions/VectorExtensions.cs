using UnityEngine;

namespace Code
{
	public static class VectorExtensions
	{
		public static Vector3 AsTopDown(this Vector2 @this) => new(x: @this.x, y: 0, z: @this.y);
	}
}
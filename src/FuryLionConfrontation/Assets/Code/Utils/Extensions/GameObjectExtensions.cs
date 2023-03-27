using UnityEngine;

namespace Confrontation
{
	public static class GameObjectExtensions
	{
		public static void DestroyComponent<T>(this GameObject @this)
			where T : Component
			=> Object.Destroy(@this.GetComponent<T>());
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace Confrontation.Editor
{
	public static class GameObjectEnumerableExtensions
	{
		public static IEnumerable<TComponent> WithComponent<TComponent>(this IEnumerable<GameObject> @this)
		{
			foreach (var gameObject in @this)
			{
				if (gameObject.TryGetComponent<TComponent>(out var component))
				{
					yield return component;
				}
			}
		}
	}
}
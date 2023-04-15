using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public static class Destroy
	{
		public static void All<T>()
			where T : MonoBehaviour
		{
			foreach (var gameObject in GetGameObjects<T>())
			{
				Object.DestroyImmediate(gameObject);
			}
		}

		private static IEnumerable<GameObject> GetGameObjects<T>()
			where T : MonoBehaviour
			=> Object.FindObjectsOfType<T>()
			         .Select((c) => c.gameObject);
	}
}
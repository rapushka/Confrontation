using System.Linq;
using UnityEngine;

namespace Confrontation.Tests
{
	public static class Destroy
	{
		public static void All<T>()
			where T : MonoBehaviour
		{
			foreach (var gameObject in Object.FindObjectsOfType<T>().Select((c) => c.gameObject))
			{
				Object.DestroyImmediate(gameObject);
			}
		}
	}
}
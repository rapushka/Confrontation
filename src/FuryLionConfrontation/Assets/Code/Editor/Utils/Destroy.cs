using System.Linq;
using UnityEngine;

namespace Confrontation.Editor
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
using UnityEngine;

namespace Confrontation.Editor
{
	public static class GameObjectExtensions
	{
		public static bool IsValidForBuilding(this GameObject gameObject, out Cell cell)
		{
			if (gameObject.TryGetComponent(out cell) == false)
			{
				Debug.LogWarning($"{gameObject.name} is not cell!");
				return false;
			}

			if (cell.IsEmpty == false)
			{
				Debug.LogWarning($"{gameObject.name} is already placed!");
				return false;
			}

			return true;
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Confrontation
{
	public static class InputTools
	{
		public static bool IsPointerOverUIObject()
		{
			if (EventSystem.current is null)
			{
				return true;
			}

			var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
			{
				position = new Vector2(Input.mousePosition.x, Input.mousePosition.y),
			};
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

			return results.Any();
		}

		public static bool IsHitReceiver(this Ray @this, out ClickReceiver receiver)
		{
			receiver = null;
			return Physics.Raycast(@this, out var hit)
			       && hit.collider.TryGetComponent(out receiver);
		}
	}
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Confrontation
{
	[RequireComponent(typeof(Collider))]
	public class MouseClickReceiver : MonoBehaviour
	{
		public event Action MouseClick;

		private void OnMouseDown()
		{
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				MouseClick?.Invoke();
			}
		}
	}
}
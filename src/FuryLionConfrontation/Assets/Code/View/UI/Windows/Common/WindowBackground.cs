using UnityEngine;
using UnityEngine.EventSystems;

namespace Confrontation
{
	public class WindowBackground : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private WindowBase _window;

		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Click");
			_window.Hide();
		}

		public void OnPointerDown(PointerEventData eventData) => Debug.Log("Down");

		public void OnPointerUp(PointerEventData eventData) => Debug.Log("Up");
	}
}
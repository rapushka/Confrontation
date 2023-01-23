using UnityEngine;
using UnityEngine.EventSystems;

namespace Confrontation
{
	public class WindowBackground : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private WindowBase _window;

		public void OnPointerClick(PointerEventData eventData) => _window.Hide();
	}
}
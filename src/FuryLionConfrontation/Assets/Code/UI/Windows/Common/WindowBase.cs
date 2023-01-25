using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private GameObject _window;

		public void Show() => _window.SetActive(true);

		public void Hide() => _window.SetActive(false);

		public class Factory : PlaceholderFactory<Object, WindowBase> { }
	}
}
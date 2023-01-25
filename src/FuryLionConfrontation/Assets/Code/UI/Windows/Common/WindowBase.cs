using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class WindowBase : MonoBehaviour
	{
		[Inject] protected readonly User User;

		[SerializeField] private GameObject _window;

		public void Show() => _window.SetActive(true);

		public void Hide() => _window.SetActive(false);

		public class Factory : PlaceholderFactory<Object, WindowBase> { }
	}
}
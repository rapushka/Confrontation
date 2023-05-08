using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private GameObject _window;

		public virtual void Open() => _window.SetActive(true);

		public virtual void Close() => _window.SetActive(false);

		public class Factory : PlaceholderFactory<GameplayWindowBase, GameplayWindowBase> { }
	}
}
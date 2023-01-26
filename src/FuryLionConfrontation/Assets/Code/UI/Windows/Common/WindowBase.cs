using UnityEngine;
using Zenject;

namespace Confrontation
{
	public abstract class WindowBase : MonoBehaviour
	{
		[Inject] protected readonly User User;

		[SerializeField] private GameObject _window;

		public virtual void Show() => _window.SetActive(true);

		public virtual void Hide() => _window.SetActive(false);

		public abstract WindowBase Accept(IWindowVisitor windowVisitor);

		public class Factory : PlaceholderFactory<Object, WindowBase> { }
	}
}
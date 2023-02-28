using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayWindows
	{
		[Inject] private readonly WindowBase.Factory _windowsFactory;
		[Inject] private readonly TypedDictionary<WindowBase> _windowsPrefabs;
		[Inject] private readonly RectTransform _canvas;

		private readonly TypedDictionary<WindowBase> _cashedWindows = new();

		[CanBeNull] private WindowBase _currentWindow;

		public void Open<TWindow>()
			where TWindow : WindowBase
		{
			CloseCurrent();
			_currentWindow = SwitchCurrentTo<TWindow>();
			_currentWindow.Open();
		}

		public void Close() => CloseCurrent();

		[NotNull]
		private WindowBase SwitchCurrentTo<TWindow>()
			where TWindow : WindowBase
			=> _cashedWindows.GetOrAdd(createNew: CreateNewWindow<TWindow>);

		private TWindow CreateNewWindow<TWindow>()
			where TWindow : WindowBase
			=> _windowsFactory.Create(_windowsPrefabs.Get<TWindow>())
			                  .With((w) => w.transform.SetParent(_canvas))
			                  .Cast<WindowBase, TWindow>();

		private void CloseCurrent()
		{
			if (_currentWindow is not null)
			{
				_currentWindow.Close();
			}
		}
	}
}
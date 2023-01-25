using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Windows
	{
		[Inject] private readonly WindowBase.FactoryBase _windowsFactory;
		[Inject] private readonly TypedDictionary<WindowBase> _windowsPrefabs;
		[Inject] private readonly IAssetsService _assets;

		private readonly TypedDictionary<WindowBase> _cashedWindows = new();
		private WindowBase _currentWindow;

		public void Show<TWindow>()
			where TWindow : WindowBase
		{
			HideCurrent();

			_currentWindow = GetOrAdd<TWindow>();
			_currentWindow.Show();
		}

		public void Hide() => HideCurrent();

		private WindowBase GetOrAdd<TWindow>() where TWindow : WindowBase
		{
			var window = _cashedWindows.GetValueOrDefault<TWindow>();
			if (window is not null)
			{
				return window;
			}

			var windowPrefab = _windowsPrefabs.Get<TWindow>();
			window = (TWindow)_windowsFactory.Create(windowPrefab);
			Debug.Log($"Created window of type {window.GetType().Name}");
			_cashedWindows.Add(window);

			return window;
		}

		private void HideCurrent()
		{
			if (_currentWindow != null)
			{
				_currentWindow.Hide();
			}
		}
	}
}
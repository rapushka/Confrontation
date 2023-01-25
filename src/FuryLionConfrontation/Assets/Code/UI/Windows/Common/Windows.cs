using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Windows
	{
		[Inject] private readonly WindowBase.FactoryBase _windowsFactory;
		[Inject] private readonly TypedDictionary<WindowBase> _windowsPrefabs;
		[Inject] private readonly IAssetsService _assets;
		[Inject] private readonly IResourcesService _resources;

		private readonly TypedDictionary<WindowBase> _cashedWindows = new();
		private WindowBase _currentWindow;
		private RectTransform _canvas;

		public void Show<TWindow>()
			where TWindow : WindowBase
		{
			HideCurrent();

			CreateCanvas();
			_currentWindow = GetOrAdd<TWindow>();
			_currentWindow.Show();
		}

		private void CreateCanvas()
		{
			if (_canvas == true)
			{
				return;
			}

			_canvas = _assets.Instantiate(_resources.Canvas);
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
			window.transform.SetParent(_canvas);
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
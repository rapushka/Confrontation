using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class Windows
	{
		[Inject] private readonly WindowBase.Factory _windowsFactory;
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

		public void Hide() => HideCurrent();

		private void CreateCanvas()
		{
			if (_canvas == true)
			{
				return;
			}

			_canvas = _assets.Instantiate(_resources.Canvas);
		}

		private WindowBase GetOrAdd<TWindow>()
			where TWindow : WindowBase
			=> _cashedWindows.ContainsKey<TWindow>()
				? _cashedWindows.Get<TWindow>()
				: CreateNewWindow<TWindow>();

		private TWindow CreateNewWindow<TWindow>()
			where TWindow : WindowBase
			=> _windowsFactory.Create(_windowsPrefabs.Get<TWindow>())
			                  .With((w) => w.transform.SetParent(_canvas))
			                  .Cast<WindowBase, TWindow>()
			                  .With(_cashedWindows.Add);

		private void HideCurrent()
		{
			if (_currentWindow != null)
			{
				_currentWindow.Hide();
			}
		}
	}
}
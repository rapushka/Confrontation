using Zenject;

namespace Confrontation
{
	public class Windows
	{
		[Inject] private readonly TypedDictionary<WindowBase> _windows;

		private WindowBase _currentWindow;

		public void Show<TWindow>()
			where TWindow : WindowBase
		{
			HideCurrent();

			_currentWindow = _windows.Get<TWindow>();
			_currentWindow.Show();
		}

		public void Hide() => HideCurrent();

		private void HideCurrent()
		{
			if (_currentWindow != null)
			{
				_currentWindow.Hide();
			}
		}
	}
}
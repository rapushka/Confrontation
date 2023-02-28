using Zenject;

namespace Confrontation
{
	public class GameplayWindows
	{
		[Inject] private readonly WindowsContainer _container;

		private WindowBase _currentWindow;

		public void Open<TWindow>()
			where TWindow : WindowBase
		{
			CloseCurrent();
			_currentWindow = _container.Windows.Get<TWindow>();
			_currentWindow.Open();
		}

		public void Close() => CloseCurrent();

		private void CloseCurrent()
		{
			if (_currentWindow is not null)
			{
				_currentWindow.Close();
			}

			_currentWindow = null;
		}
	}
}
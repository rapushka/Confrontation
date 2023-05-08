using Zenject;

namespace Confrontation
{
	public class WindowsStack<T>
		where T : WindowBase
	{
		[Inject] private readonly WindowsContainer _container;

		private T _currentWindow;

		public void Open<TWindow>()
			where TWindow : T
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
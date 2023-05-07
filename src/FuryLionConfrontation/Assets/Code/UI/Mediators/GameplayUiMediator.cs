using Zenject;

namespace Confrontation
{
	public class GameplayUiMediator
	{
		[Inject] private readonly GameplayWindows _gameplayWindows;

		public void OpenWindow<T>() where T : GameplayWindowBase => _gameplayWindows.Open<T>();

		public void CloseCurrentWindow() => _gameplayWindows.Close();
	}
}
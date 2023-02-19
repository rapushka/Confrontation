using Zenject;

namespace Confrontation
{
	public class GameplayUiMediator
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;
		[Inject] private readonly GameplayWindows _gameplayWindows;
		[Inject] private readonly Hud _hud;

		public void Build(Building building) => _buildingSpawner.Build(building);

		public void OpenWindow<T>() where T : WindowBase => _gameplayWindows.Open<T>();

		public void CloseCurrentWindow() => _gameplayWindows.Close();
	}
}
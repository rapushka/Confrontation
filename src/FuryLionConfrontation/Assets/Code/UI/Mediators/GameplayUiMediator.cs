using Zenject;

namespace Confrontation
{
	public class GameplayUiMediator : IUiMediator
	{
		[Inject] private readonly BuildingSpawner _buildingSpawner;
		[Inject] private readonly Windows _windows;

		public void Build(Building building) => _buildingSpawner.Build(building);

		public void OpenWindow<T>() where T : WindowBase => _windows.Open<T>();

		public void CloseCurrentWindow() => _windows.Close();
	}
}
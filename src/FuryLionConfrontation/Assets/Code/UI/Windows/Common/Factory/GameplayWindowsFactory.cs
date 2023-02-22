using Zenject;

namespace Confrontation
{
	public class GameplayWindowsFactory : IFactory<WindowBase, WindowBase>, IWindowVisitor
	{
		[Inject] private readonly BuildWindow.Factory _buildWindowFactory;
		[Inject] private readonly BuildingInfoWindow.Factory _buildingWindowFactory;
		[Inject] private readonly GameResultsWindow.Factory _gameResultWindowFactory;
		[Inject] private readonly NotEnoughGoldWindow.Factory _notEnoughGoldWindowFactory;

		private WindowBase _window;

		public WindowBase Create(WindowBase windowPrefab) => windowPrefab.Accept(this);

		public WindowBase Visit(BuildWindow window) => _buildWindowFactory.Create(window);

		public WindowBase Visit(BuildingInfoWindow window) => _buildingWindowFactory.Create(window);

		public WindowBase Visit(GameResultsWindow window) => _gameResultWindowFactory.Create(window);

		public WindowBase Visit(NotEnoughGoldWindow window) => _notEnoughGoldWindowFactory.Create(window);
	}
}
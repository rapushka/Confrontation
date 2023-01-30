using Zenject;

namespace Confrontation
{
	public class GameplayWindowsFactory : IFactory<WindowBase, WindowBase>, IWindowVisitor
	{
		[Inject] private readonly BuildWindow.Factory _buildWindowFactory;
		[Inject] private readonly BuildingWindow.Factory _buildingWindowFactory;

		private WindowBase _window;

		public WindowBase Create(WindowBase windowPrefab) => windowPrefab.Accept(this);

		public WindowBase Visit(BuildWindow window) => _buildWindowFactory.Create(window);

		public WindowBase Visit(BuildingWindow window) => _buildingWindowFactory.Create(window);
	}
}
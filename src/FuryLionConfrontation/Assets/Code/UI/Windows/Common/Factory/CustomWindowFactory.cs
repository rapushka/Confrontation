using Zenject;
using Object = UnityEngine.Object;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Confrontation
{
	public class CustomWindowFactory : PrefabFactory<WindowBase>, IWindowVisitor
	{
		[Inject] private readonly BuildWindow.Factory _buildWindowFactory;
		[Inject] private readonly BuildingWindow.Factory _buildingWindowFactory;

		private WindowBase _window;

		public override WindowBase Create(Object window) => ((WindowBase)window).Accept(this);

		public WindowBase Visit(BuildWindow window) => _buildWindowFactory.Create(window);

		public WindowBase Visit(BuildingWindow window) => _buildingWindowFactory.Create(window);
	}
}
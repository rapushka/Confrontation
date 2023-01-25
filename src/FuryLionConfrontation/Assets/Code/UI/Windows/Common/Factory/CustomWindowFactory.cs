using System;
using Zenject;
using Object = UnityEngine.Object;

namespace Confrontation
{
	public class CustomWindowFactory : PrefabFactory<WindowBase>
	{
		[Inject] private readonly BuildWindow.Factory _buildWindowFactory;
		[Inject] private readonly BuildingWindow.Factory _buildingWindowFactory;

		public override WindowBase Create(Object window)
			=> window switch
			{
				BuildWindow    => _buildWindowFactory.Create(window),
				BuildingWindow => _buildingWindowFactory.Create(window),
				var _          => throw new ArgumentException(),
			};
	}
}
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller<GameplayInstaller>
	{
		[SerializeField] private BuildingButton _buildingButtonPrefab;
		[SerializeField] private UnitsSquad _unitPrefab;
		[SerializeField] private LineRenderer _orderLineRenderer;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInstance(_orderLineRenderer).AsSingle();

			Container.Bind<IField>().To<Field>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			Container.BindInterfacesAndSelfTo<Orders>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldClicksHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<CoolDownActionsHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<OrderDirectionLineRenderer>().AsSingle();

			Container.Bind<BuildingSpawner>().AsSingle();
			Container.Bind<GameplayUiMediator>().AsSingle();
			Container.Bind<GameplayWindows>().AsSingle();

			BindFactory();
		}

		private void BindFactory()
		{
			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingWindow, BuildingWindow.Factory>();
			Container.BindFactory<Building, BuildingButton, BuildingButton.Factory>()
			         .FromComponentInNewPrefab(_buildingButtonPrefab);

			Container.BindFactory<Building, Building, Building.Factory>().FromFactory<PrefabFactory<Building>>();

			Container.BindFactory<WindowBase, WindowBase, WindowBase.Factory>().FromFactory<GameplayWindowsFactory>();

			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromComponentInNewPrefab(_unitPrefab);
		}
	}
}
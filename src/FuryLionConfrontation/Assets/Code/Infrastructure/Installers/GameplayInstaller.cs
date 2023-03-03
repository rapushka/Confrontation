using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller<GameplayInstaller>
	{
		[SerializeField] private BuildingButton _buildingButtonPrefab;
		[SerializeField] private UnitsSquad _unitPrefab;
		[SerializeField] private Garrison _garrisonPrefab;
		[SerializeField] private LineRenderer _orderLineRenderer;
		[SerializeField] private Cell _cellPrefab;
		[SerializeField] private BackToMenuButton _backToMenuButton;
		[SerializeField] private AccelerateTimeButton _accelerateTimeButton;
		[SerializeField] private CameraSwipeMovement _movement;
		[SerializeField] private Hud _hud;
		[SerializeField] private RectTransform _canvas;
		[SerializeField] private WindowsContainer _windowsContainer;

		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<WindowsContainer>().FromInstance(_windowsContainer).AsSingle();
			Container.BindInstance(_canvas).AsSingle();
			Container.BindInstance(_orderLineRenderer).AsSingle();
			Container.BindInstance(_movement).AsSingle();
			Container.BindInterfacesAndSelfTo<Hud>().FromInstance(_hud).AsSingle();

			Container.Bind<IField>().To<Field>().AsSingle();

			Container.Bind<ToMainMenu>().AsSingle();
			Container.Bind<Purchase>().AsSingle();
			Container.BindInstance(_backToMenuButton).AsSingle();
			Container.BindInterfacesTo<AccelerateTimeButton>().FromInstance(_accelerateTimeButton).AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsNeighboringCalculator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayersGenerator>().AsSingle();

			Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
			Container.BindInterfacesAndSelfTo<ArtificialIntelligence>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldBounds>().AsSingle();

			Container.BindInterfacesAndSelfTo<Orders>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldInputHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<CoolDownActionsHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<OrderDirectionLineDrawer>().AsSingle();

			Container.Bind<BuildingSpawner>().AsSingle();
			Container.Bind<GameplayUiMediator>().AsSingle();
			Container.Bind<GameplayWindows>().AsSingle();

			BindFactories();
		}

		private void BindFactories()
		{
			Container.BindFactory<Player, Enemy, Enemy.Factory>()
			         .FromSubContainerResolve()
			         .ByInstaller<EnemyInstaller>();

			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingInfoWindow, BuildingInfoWindow.Factory>();
			Container.BindPrefabFactory<GameResultsWindow, GameResultsWindow.Factory>();
			Container.BindPrefabFactory<NotEnoughGoldWindow, NotEnoughGoldWindow.Factory>();
			Container.BindFactory<Building, BuildingButton, BuildingButton.Factory>()
			         .FromComponentInNewPrefab(_buildingButtonPrefab);

			Container.BindFactory<Building, Building, Building.Factory>().FromFactory<PrefabFactory<Building>>();

			Container.BindFactory<WindowBase, WindowBase, WindowBase.Factory>().FromFactory<GameplayWindowsFactory>();

			Container.BindFactory<Garrison, Garrison.Factory>().FromComponentInNewPrefab(_garrisonPrefab);
			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromComponentInNewPrefab(_unitPrefab);
			Container.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(_cellPrefab);
			Container.BindFactory<int, Region, Region.Factory>();
		}
	}
}
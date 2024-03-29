using UnityEngine;

namespace Confrontation
{
	public class GameplayInstaller : GameFieldInstaller
	{
		[SerializeField] private BuildingButton _buildingButtonPrefab;
		[SerializeField] private SpellButton _spellButtonPrefab;
		[SerializeField] private UnitsSquad _unitPrefab;
		[SerializeField] private Garrison _garrisonPrefab;
		[SerializeField] private LineRenderer _orderLineRenderer;
		[SerializeField] private GameplayCameraSwipeMovement _movement;
		[SerializeField] private AccelerateTimeToggle _accelerateTimeToggle;
		[SerializeField] private Hud _hud;
		[SerializeField] private RectTransform _buildingButtonsRoot;
		[SerializeField] private RectTransform _spellButtonsRoots;
		[SerializeField] private AudioSettingsSection _audioSettingsSection;

		protected override void InstallSpecificBindings()
		{
			Container.BindInstance(_movement).AsSingle();

			Container.Bind<IField>().To<Field>().AsSingle();
			Container.Bind<IPurchase>().To<Purchase>().AsSingle();

			Container.BindInterfacesAndSelfTo<Hud>().FromInstance(_hud).AsSingle();
			Container.BindInterfacesTo<AccelerateTimeToggle>().FromInstance(_accelerateTimeToggle).AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsNeighborhoodCalculator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsBordersCalculator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<PlayersGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();

			Container.BindInterfacesTo<FieldBounds>().AsSingle();

			Container.BindInterfacesAndSelfTo<ArtificialIntelligence>().AsSingle();

			Container.BindInterfacesAndSelfTo<Orders>().AsSingle();
			Container.BindInstance(_orderLineRenderer).AsSingle();
			Container.BindInterfacesAndSelfTo<OrderDirectionLineDrawer>().AsSingle();

			Container.BindInterfacesAndSelfTo<GameplayFieldInputDirector>().AsSingle();
			Container.BindInterfacesAndSelfTo<CoolDownActionsHandler>().AsSingle();

			Container.Bind<BuildingSpawner>().AsSingle();
			Container.Bind<GameplayUiMediator>().AsSingle();
			Container.Bind<GameplayWindows>().AsSingle();
			Container.Bind<SpellCaster>().AsSingle();

			Container.BindInterfacesAndSelfTo<Tutorial>().AsSingle();

			Container.BindInterfacesTo<AudioSettingsSection>().FromInstance(_audioSettingsSection).AsTransient();
		}

		protected override void InstallSpecificFactories()
		{
			Container.BindFactory<Player, Enemy, Enemy.Factory>()
			         .FromSubContainerResolve()
			         .ByInstaller<EnemyInstaller>();

			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingInfoWindow, BuildingInfoWindow.Factory>();
			Container.BindPrefabFactory<GameResultsWindow, GameResultsWindow.Factory>();
			Container.BindPrefabFactory<NotEnoughGoldWindow, NotEnoughGoldWindow.Factory>();
			Container.BindPrefabFactory<SpellBookWindow, SpellBookWindow.Factory>();
			Container.BindPrefabFactory<TutorialWindow, TutorialWindow.Factory>();

			Container.BindFactory<GameplayWindowBase, GameplayWindowBase, WindowBase.Factory>()
			         .FromFactory<GameplayWindowsFactory>();

			Container.BindFactory<Building, BuildingButton, BuildingButton.Factory>()
			         .FromComponentInNewPrefab(_buildingButtonPrefab)
			         .UnderTransform(_buildingButtonsRoot);

			Container.BindFactory<ISpell, ToolTip, SpellButton, SpellButton.Factory>()
			         .FromComponentInNewPrefab(_spellButtonPrefab)
			         .UnderTransform(_spellButtonsRoots);

			Container.BindFactory<Garrison, Garrison.Factory>().FromComponentInNewPrefab(_garrisonPrefab);
			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromComponentInNewPrefab(_unitPrefab);
		}
	}
}
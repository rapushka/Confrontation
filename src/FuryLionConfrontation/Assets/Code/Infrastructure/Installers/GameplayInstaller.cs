using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private BuildingButton _buildingButton;

		// ReSharper disable Unity.PerformanceAnalysis - Method call only on initialization
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<Field>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			Container.BindInterfacesAndSelfTo<Orders>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldClicksHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<CoolDownActionsHandler>().AsSingle();

			Container.Bind<BuildingSpawner>().AsSingle();
			Container.Bind<GameplayUiMediator>().AsSingle();

			BindFactory();
		}

		private void BindFactory()
		{
			Container.BindPrefabFactory<BuildWindow, BuildWindow.Factory>();
			Container.BindPrefabFactory<BuildingWindow, BuildingWindow.Factory>();
			Container.BindFactory<Building, BuildingButton, BuildingButton.Factory>()
			         .FromComponentInNewPrefab(_buildingButton);

			Container.BindFactory<Building, Building, Building.Factory>()
			         .FromFactory<PrefabFactory<Building>>();
		}
	}
}
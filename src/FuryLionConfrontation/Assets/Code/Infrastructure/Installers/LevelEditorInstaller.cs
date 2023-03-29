using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		[SerializeField] private RegionEntry _regionEntryPrefab;
		[SerializeField] private RegionOwnershipEntry _regionOwnershipEntryPrefab;
		[SerializeField] private LevelEditorTabsSystem _tabs;
		[SerializeField] private BuildingEntry _buildingEntryPrefab;
		

		protected override void InstallSpecificBindings()
		{
			Container.BindInstance(_tabs).AsSingle();

			Container.Bind<BuildingSpawner>().AsSingle();

			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();

			Container.BindInterfacesAndSelfTo<ConfigurableField>().AsSingle();

			Container.Bind<ISession>().To<LevelEditSession>().AsSingle();
			Container.Bind<ILevelSaver>().To<LevelSaver>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<ReinitializableRegionsBordersCalculator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			Container.BindInterfacesAndSelfTo<LevelEditorFieldInputDirector>().AsSingle();
			Container.BindInterfacesAndSelfTo<OutlineCellsInCurrentRegion>().AsSingle();
		}

		protected override void InstallSpecificFactories()
		{
			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromMethod(DoNothing<UnitsSquad>);
			Container.BindFactory<Garrison, Garrison.Factory>().FromMethod(DoNothing<Garrison>);

			Container.BindFactory<Region, RegionEntry, RegionEntry.Factory>()
			         .FromComponentInNewPrefab(_regionEntryPrefab);

			Container.BindFactory<Region, RegionOwnershipEntry, RegionOwnershipEntry.Factory>()
			         .FromComponentInNewPrefab(_regionOwnershipEntryPrefab);

			Container.BindFactory<Building, BuildingEntry, BuildingEntry.Factory>()
			         .FromComponentInNewPrefab(_buildingEntryPrefab);
		}

		private T DoNothing<T>(DiContainer container) => throw new Exception("This method must not be called");
	}
}
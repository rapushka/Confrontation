using System;
using UnityEngine;
using Zenject;

namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		[SerializeField] private RegionEntry _regionEntryPrefab;
		[SerializeField] private LevelEditorTabsSystem _tabs;

		protected override void InstallSpecificBindings()
		{
			Container.BindInstance(_tabs).AsSingle();
			
			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();

			Container.BindInterfacesAndSelfTo<ConfigurableField>().AsSingle();

			Container.Bind<ISession>().To<LevelEditSession>().AsSingle();
			Container.Bind<ILevelSaver>().To<LevelSaver>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();
			
			Container.BindInterfacesAndSelfTo<LevelEditorFieldInputDirector>().AsSingle();
		}

		protected override void InstallSpecificFactories()
		{
			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromMethod(DoNothing<UnitsSquad>);
			Container.BindFactory<Garrison, Garrison.Factory>().FromMethod(DoNothing<Garrison>);

			Container.BindFactory<int, RegionEntry, RegionEntry.Factory>()
			         .FromComponentInNewPrefab(_regionEntryPrefab);
		}

		private T DoNothing<T>(DiContainer container) => throw new Exception("This method must not be called");
	}
}
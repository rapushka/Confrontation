using System;
using Zenject;

namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		protected override void InstallSpecificBindings()
		{
			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();

			Container.BindInterfacesAndSelfTo<ConfigurableField>().AsSingle();

			Container.Bind<ISession>().To<LevelEditSession>().AsSingle();
			Container.Bind<ILevelSaver>().To<LevelSaver>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			Container.BindFactory<UnitsSquad, UnitsSquad.Factory>().FromMethod(DoNothing<UnitsSquad>);
			Container.BindFactory<Garrison, Garrison.Factory>().FromMethod(DoNothing<Garrison>);
		}

		private T DoNothing<T>(DiContainer container) => throw new Exception("This method must not be called");

		protected override void InstallSpecificFactories() { }
	}
}
namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		protected override void InstallSpecificBindings()
		{
			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();

			Container.Bind<IField>().To<ConfigurableField>().AsSingle();

			Container.Bind<ISession>().To<LevelEditSession>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
		}

		protected override void InstallSpecificFactories() { }
	}
}
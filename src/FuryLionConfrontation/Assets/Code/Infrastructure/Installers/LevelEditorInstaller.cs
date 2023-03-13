namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		protected override void InstallSpecificBindings()
		{
			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();

			Container.Bind<IField>().To<ConfigurableField>().AsSingle();

			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
		}

		protected override void InstallSpecificFactories() { }
	}
}
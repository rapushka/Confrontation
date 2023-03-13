namespace Confrontation
{
	public class LevelEditorInstaller : GameFieldInstaller
	{
		protected override void InstallSpecificBindings()
		{
			Container.Bind<IFieldBounds>().To<EditorFieldBounds>().AsSingle();
		}

		protected override void InstallSpecificFactories() { }
	}
}
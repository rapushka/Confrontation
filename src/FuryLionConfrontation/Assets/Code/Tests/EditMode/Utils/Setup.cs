using Zenject;

namespace Confrontation.Editor.Tests
{
	public static class Setup
	{
		public static void SetupFieldGenerator(this DiContainer @this)
		{
			@this.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			@this.Bind<IResourcesService>().FromInstance(Create.ResourcesService()).AsSingle();
			@this.Bind<ILevelSelector>().To<LevelCreator>().AsSingle();
			@this.Bind<Field>().AsSingle();
			@this.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
		}
	}
}

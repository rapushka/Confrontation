using Zenject;

namespace Confrontation.Editor.Tests
{
	public static class BindExtensions
	{
		public static void BindFieldGenerator(this DiContainer @this)
		{
			@this.Bind<IResourcesService>().FromInstance(Create.ResourcesService()).AsSingle();
			@this.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			@this.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
		}

		public static void BindField(this DiContainer @this)
		{
			@this.Bind<ILevelSelector>().To<TestLevelCreator>().AsSingle();
			@this.Bind<Field>().AsSingle();
		}

		public static void BindRegionsGenerator(this DiContainer @this)
		{
			@this.BindFactory<Building, Building, Building.Factory>()
			         .FromFactory<PrefabFactory<Building>>();
			@this.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
		}
	}
}
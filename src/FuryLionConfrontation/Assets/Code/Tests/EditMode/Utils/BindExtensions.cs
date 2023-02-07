using UnityEngine;
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
			@this.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(Resources.Load<Cell>("Prefabs/Cell"));
		}

		public static void BindField(this DiContainer @this)
		{
			@this.Bind<ILevelSelector>().To<TestLevelCreator>().AsSingle();
			@this.Bind<IField>().To<Field>().AsSingle();
		}

		public static void BindRegionsGenerator(this DiContainer @this)
		{
			@this.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			@this.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			@this.BindFactory<Building, Building, Building.Factory>()
			     .FromFactory<PrefabFactory<Building>>();
			@this.BindFactory<Region, Region.Factory>();
		}
	}
}
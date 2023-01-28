using NSubstitute;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	public static class BindExtensions
	{
		private const string FieldColor = "_color";

		public static void BindFieldGenerator(this DiContainer @this)
		{
			var resourcesService = Create.ResourcesService();
			var cell = Cell();
			resourcesService.CellPrefab.Returns(cell);
			
			@this.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			@this.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			@this.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
		}

		private static Cell Cell()
		{
			var cell = Create.Cell();
			var regionColor = cell.gameObject.AddComponent<RegionColor>();
			regionColor.ChangeMaterialTo(default);
			cell.SetPrivateField(FieldColor, regionColor);
			return cell;
		}

		public static void BindField(this DiContainer @this)
		{
			@this.Bind<ILevelSelector>().To<TestLevelCreator>().AsSingle();
			@this.Bind<Field>().AsSingle();
		}

		public static void BindRegionsGenerator(this DiContainer @this)
		{
			@this.BindFactory<Building, Transform, int, Building, Building.Factory>()
			     .FromFactory<CustomBuildingFactory>();
			@this.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
		}
	}
}
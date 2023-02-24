using NSubstitute;
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
			@this.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefabResource(Constants.ResourcePath.Cell);
		}

		public static void BindRegionsGenerator(this DiContainer @this)
		{
			@this.Bind<GameplayUiMediator>().FromSubstitute().AsSingle();

			@this.Bind<User>().FromInstance(Setup.User()).AsSingle();

			@this.BindInterfacesAndSelfTo<GameSession>().AsSingle();
			@this.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
			@this.BindInterfacesAndSelfTo<RegionsNeighboringCalculator>().AsSingle();
			@this.BindInterfacesAndSelfTo<BuildingsGenerator>().AsSingle();

			@this.BindInterfacesAndSelfTo<PlayersGenerator>().AsSingle();

			@this.BindFactory<Building, Building, Building.Factory>()
			     .FromFactory<PrefabFactory<Building>>();
			@this.BindFactory<int, Region, Region.Factory>();
			@this.BindFactory<Garrison, Garrison.Factory>()
			     .FromComponentInNewPrefabResource(Constants.ResourcePath.Garrison);
		}

		public static void BindLevelAt(this DiContainer @this, string levelPath)
		{
			var level = Resources.Load<LevelScriptableObject>(levelPath);
			var levelSelector = Substitute.For<ILevelSelector>();
			levelSelector.SelectedLevel.Returns(level);
			@this.Bind<ILevelSelector>().FromInstance(levelSelector).AsSingle();
		}
	}
}
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class RegionsNeighboringCalculationTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			Container.Bind<IBalanceTable>().FromSubstitute();

			Container.Bind<IField>().To<Field>().AsSingle();
			Container.BindFieldGenerator();
			Container.BindRegionsGenerator();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();

			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void _1_WhenCalculateNeighboring_AndThereIsCellWith1NeighborRegion_ThenNeighboringShouldContain2Elements()
		{
			// Arrange.
			var level = Resources.Load<LevelScriptableObject>(TestLevelName.LevelWithRegions);
			var levelSelector = Substitute.For<ILevelSelector>();
			levelSelector.SelectedLevel.Returns(level);
			Container.Bind<ILevelSelector>().FromInstance(levelSelector).AsSingle();
			
			Container.Resolve<FieldGenerator>().Initialize();
			Container.Resolve<RegionsGenerator>().Initialize();
			
			var field = Container.Resolve<IField>();
			var calculator = Container.Resolve<RegionsNeighboringCalculator>();

			// Act.
			calculator.Initialize();

			// Assert.
			var countOfNeighborhoods = field.Neighboring.Neighbouring.Count;
			countOfNeighborhoods.Should().Be(2);
		}
	}

	public static class TestLevelName
	{
		public const string LevelWithRegions = PathToFolder + "Level With 2 Regions";
		private const string PathToFolder = "ScriptableObjects/For Tests/Levels By Regions/";
	}
}
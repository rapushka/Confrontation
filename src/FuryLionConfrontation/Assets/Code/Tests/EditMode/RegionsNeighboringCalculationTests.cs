using FluentAssertions;
using NUnit.Framework;
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
		public void
			_1_WhenCalculateNeighboring_AndThereIsCellWith1NeighborRegion_ThenNeighboringShouldContain2Elements()
		{
			// Arrange.
			Container.BindLevelAt(TestLevelName.LevelWithRegions);

			Container.Resolve<FieldGenerator>().Initialize();
			Container.Resolve<RegionsGenerator>().Initialize();

			var field = Container.Resolve<IField>();
			var calculator = Container.Resolve<RegionsNeighboringCalculator>();

			// Act.
			calculator.Initialize();

			// Assert.
			var countOfNeighborhoods = field.Neighboring.Neighborhoods.Length;
			countOfNeighborhoods.Should().Be(2);
		}

		[Test]
		public void
			_2_WhenCalculateNeighboring_AndThereIs2PairOfCellNearby_ThenNeighboringForFirstRegionMustContain2Elements()
		{
			// Arrange.
			Container.BindLevelAt(TestLevelName.LevelWithRegions);

			Container.Resolve<FieldGenerator>().Initialize();
			Container.Resolve<RegionsGenerator>().Initialize();

			var field = Container.Resolve<IField>();
			var calculator = Container.Resolve<RegionsNeighboringCalculator>();

			// Act.
			calculator.Initialize();

			// Assert.
			var countOfNeighborsForFirstRegion = 0;
			for (var i = 0; i < field.Neighboring.Neighborhoods.GetLength(1); i++)
			{
				countOfNeighborsForFirstRegion += field.Neighboring.Neighborhoods[0, i] ? 1 : 0;
			}

			countOfNeighborsForFirstRegion.Should().Be(2);
		}

		[Test]
		public void
			_3_GenerateRegionNeighboringFor3rdLevel()
		{
			// Arrange.
			Container.BindLevelAt("ScriptableObjects/For Tests/Levels By Regions/Level With 5 Regions");

			Container.Resolve<FieldGenerator>().Initialize();
			Container.Resolve<RegionsGenerator>().Initialize();

			var field = Container.Resolve<IField>();
			var calculator = Container.Resolve<RegionsNeighboringCalculator>();

			// Act.
			calculator.Initialize();

			// Assert.
			var countOfNeighborsForFirstRegion = 0;
			for (var i = 0; i < field.Neighboring.Neighborhoods.GetLength(1); i++)
			{
				countOfNeighborsForFirstRegion += field.Neighboring.Neighborhoods[0, i] ? 1 : 0;
			}

			countOfNeighborsForFirstRegion.Should().Be(2);
		}
	}

	public static class TestLevelName
	{
		public const string LevelWithRegions = PathToFolder + "Level With 2 Regions";
		private const string PathToFolder = "ScriptableObjects/For Tests/Levels By Regions/";
	}
}
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class RegionsGeneratorTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			Container.BindField();
			Container.BindFieldGenerator();
			Container.BindRegionsGenerator();

			Container.Resolve<FieldGenerator>().Initialize();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();

			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void _1_WhenGenerateField_AndLevelContain1Region_ThenShouldBe1Village()
		{
			// Arrange.
			var buildingsGenerator = Container.Resolve<BuildingsGenerator>();

			// Act.
			buildingsGenerator.Initialize();

			// Assert.
			var countOfVillages = Object.FindObjectsOfType<Village>().Length;
			countOfVillages.Should().Be(1);
		}

		[Test]
		public void
			_2_WhenGenerateField_AndLevelContainRegionWithVillageAndCellOnSeparateCell_ThenVillageShouldHave2CellsInRegion()
		{
			// Arrange.
			var field = Container.Resolve<IField>();
			var buildingsGenerator = Container.Resolve<BuildingsGenerator>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			buildingsGenerator.Initialize();
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.Buildings.OfType<Village>().Single().CellsInRegion.ToList().Count;
			countOfCellsInRegion.Should().Be(2);
		}

		[Test]
		public void _2_1_WhenGenerateField_AndCellOnPosition1to1_ThenCellCoordinatesShouldBe1to1()
		{
			// Arrange.
			var field = Container.Resolve<IField>();
			var fieldGenerator = Container.Resolve<FieldGenerator>();

			// Act.
			fieldGenerator.Initialize();

			// Assert.
			var cell = field.Cells[new Coordinates(1, 1)];
			cell.Coordinates.Should().Be(new Coordinates(1, 1));
		}
	}
}
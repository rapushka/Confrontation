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
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

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
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.Buildings.OfType<Village>().Single().CellsInRegion.ToList().Count;
			countOfCellsInRegion.Should().Be(2);
		}

		[Test]
		public void _3_WhenGenerateField_AndLevelContainRegion_ThenVillageShouldSelfCellInSelfRegion()
		{
			// Arrange.
			var field = Container.Resolve<IField>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

			// Assert.
			var village = field.Buildings.OfType<Village>().Single();
			var cellsInRegion = village.CellsInRegion.ToList();
			cellsInRegion.Should().Contain(village.RelatedCell);
		}
	}
}
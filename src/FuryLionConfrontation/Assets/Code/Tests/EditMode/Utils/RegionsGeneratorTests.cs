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
		public void WhenGenerateField_AndLevelContain1Region_ThenShouldBe1Village()
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
		public void WhenGenerateField_AndLevelContainRegionWith7Cells_ThenVillageShouldHave7CellsInRegion()
		{
			// Arrange.
			var field = Container.Resolve<ConfigurableField>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(1);
		}

		[Test]
		public void WhenGenerateField_AndVillageAndCellInRegionOnSameCell_ThenVillageShouldHave7CellsInRegion()
		{
			// Arrange.
			var field = Container.Resolve<ConfigurableField>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(1);
		}
	}
}
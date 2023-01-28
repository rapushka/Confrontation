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
			Container.BindFactory<Building, Transform, int, Building, Building.Factory>()
			         .FromFactory<CustomBuildingFactory>();
			Container.BindInterfacesAndSelfTo<RegionsGenerator>().AsSingle();
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
			var field = Container.Resolve<Field>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			// field.Initialize();
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(7);
		}

		[Test]
		public void WhenGenerateField_AndVillageAndCellInRegionOnSameCell_ThenVillageShouldHave7CellsInRegion()
		{
			// Arrange.
			var field = Container.Resolve<Field>();
			var regions = Container.Resolve<RegionsGenerator>();

			// Act.
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(7);
		}
	}
}
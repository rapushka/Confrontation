using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class FieldGeneratorTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			Container.Bind<ILevelSelector>().To<LevelCreator>().AsSingle();

			Container.Bind<Field>().AsSingle();
			Container.BindInterfacesAndSelfTo<FieldGenerator>().AsSingle();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();

			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void WhenInitialized_AndFieldWasEmpty_ThenFieldShouldNotContainEmptyElements()
		{
			// Arrange.
			var field = Container.Resolve<Field>();
			var generator = Container.Resolve<FieldGenerator>();

			// Act.
			generator.Initialize();

			// Assert.
			var cells = field.Cells;
			cells.Should().AllBeOfType<Cell>();
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
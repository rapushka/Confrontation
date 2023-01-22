using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class FieldTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			var resourcesService = Resources.Load<ResourcesService>("ScriptableObjects/Resources");
			
			Container.Bind<IAssetsService>().To<AssetsService>().AsSingle();
			Container.Bind<IResourcesService>().FromInstance(resourcesService).AsSingle();
			Container.Bind<Field>().AsSingle();
			Container.Bind<Regions>().AsSingle();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();

			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void WhenGenerateField_AndIsWasEmpty_ThenShouldNotContainNullElements()
		{
			// Arrange.
			var field = Container.Resolve<Field>();

			// Act.
			field.Initialize();

			// Assert.
			var cells = field.GetCells();
			cells.All((c) => c is not null).Should().BeTrue();
		}

		[Test]
		public void WhenGenerateField_AndLevelContain1Region_ThenShouldBe1Village()
		{
			// Arrange.
			var field = Container.Resolve<Field>();
			var regions = Container.Resolve<Regions>();

			// Act.
			field.Initialize();
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
			var regions = Container.Resolve<Regions>();

			// Act.
			field.Initialize();
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
			var regions = Container.Resolve<Regions>();

			// Act.
			field.Initialize();
			regions.Initialize();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(7);
		}
	}
}
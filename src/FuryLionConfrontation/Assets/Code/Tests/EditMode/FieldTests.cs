using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public class FieldTests
	{
		[TearDown]
		public void TearDown()
		{
			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void WhenGenerateField_AndIsWasEmpty_ThenShouldNotContainNullElements()
		{
			// Arrange.
			var field = Setup.Field();

			// Act.
			field.GenerateField();

			// Assert.
			var cells = field.GetCells();
			cells.All((c) => c is not null).Should().BeTrue();
		}

		[Test]
		public void WhenGenerateField_AndLevelContain1Region_ThenShouldBe1Village()
		{
			// Arrange.
			var field = Setup.Field();
			field.AddRegion(Setup.Region());

			// Act.
			field.GenerateField();

			// Assert.
			var countOfVillages = Object.FindObjectsOfType<Village>().Length;
			countOfVillages.Should().Be(1);
		}

		[Test]
		public void WhenGenerateField_AndLevelContainRegionWith1Cells_ThenVillageMustHave1CellsInRegion()
		{
			// Arrange.
			var field = Setup.Field(height: 2, width: 2);
			field.AddRegion(Setup.Region(row: 0, column: 0));
			field.AddCellToFirstRegion(Setup.Cell(row: 0, column: 1));

			// Act.
			field.GenerateField();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(1);
		}

		[Test]
		public void WhenGenerateField_AndVillageAndCellInRegionOnSameCell_ThenVillageMustHave1CellsInRegion()
		{
			// Arrange.
			var field = Setup.Field();
			field.AddRegion(Setup.Region(row: 0, column: 0));
			field.AddCellToFirstRegion(Setup.Cell(row: 0, column: 0));

			// Act.
			field.GenerateField();

			// Assert.
			var countOfCellsInRegion = field.GetVillages().Single().CellsInRegion.Count;
			countOfCellsInRegion.Should().Be(1);
		}
	}
}
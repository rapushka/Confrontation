using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Confrontation.Editor.Tests
{
	public class LevelEditorTests
	{
		[Test]
		public void WhenGenerateField_Once_ThenShouldBeSingleField()
		{
			// Arrange.
			var levelEditor = Create.LevelEditor();

			// Act.
			levelEditor.GenerateField(1, 1);

			// Assert.
			var countOfRoots = Object.FindObjectsOfType<CellsRoot>().Length;
			countOfRoots.Should().Be(1);
		}

		[Test]
		public void WhenGenerateField_Twice_ThenShouldBeSingleField()
		{
			// Arrange.
			var levelEditor = Create.LevelEditor();

			// Act.
			levelEditor.GenerateField(1, 1);
			levelEditor.GenerateField(1, 1);

			// Assert.
			var countOfRoots = Object.FindObjectsOfType<CellsRoot>().Length;
			countOfRoots.Should().Be(1);
		}

		[Test]
		public void WhenGenerateField_And2By3_ThenShouldBe6Cells()
		{
			// Arrange.
			const int height = 2;
			const int width = 3;
			var levelEditor = Create.LevelEditor();

			// Act.
			levelEditor.GenerateField(height, width);

			// Assert.
			var countOfCells = Object.FindObjectsOfType<Cell>().Length;
			countOfCells.Should().Be(height * width);
		}

		[Test]
		public void WhenGenerateField_AndTwice_ThenCellsCountShouldBeSame()
		{
			// Arrange.
			const int height = 2;
			const int width = 3;
			var levelEditor = Create.LevelEditor();

			// Act.
			levelEditor.GenerateField(height, width);
			levelEditor.GenerateField(height, width);

			// Assert.
			var countOfCells = Object.FindObjectsOfType<Cell>().Length;
			countOfCells.Should().Be(height * width);
		}

		[Test]
		public void WhenToVillage_AndOnEmptyCell_ThenCreateVillage()
		{
			// Arrange.
			var levelEditor = Setup.LevelEditor();
			var cell = Setup.Cell();

			// Act.
			levelEditor.ToVillage(cell);

			// Assert.
			var countOfVillages = cell.GetComponentsInChildren<Village>().Length;
			countOfVillages.Should().Be(1);
		}

		[Test]
		public void WhenToVillage_AndOnCellWithVillage_ThenShouldBe1Village()
		{
			// Arrange.
			var levelEditor = Setup.LevelEditor();
			var cell = Setup.Cell();

			// Act.
			levelEditor.ToVillage(cell);
			levelEditor.ToVillage(cell);

			// Assert.
			var countOfVillages = cell.GetComponentsInChildren<Village>().Length;
			countOfVillages.Should().Be(1);
		}
	}
}
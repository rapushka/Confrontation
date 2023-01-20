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
			var cells = field.GetCells().Cast<Cell>();
			cells.All((c) => c is not null).Should().BeTrue();
		}

		[Test]
		public void WhenGenerateField_AndLevelContain1Region_ThenShouldBe1Village()
		{
			// Arrange.
			var field = Setup.Field();
			field.AddRegion(new Coordinates(1, 1));

			// Act.
			field.GenerateField();

			// Assert.
			var countOfVillages = Object.FindObjectsOfType<Village>().Length;
			countOfVillages.Should().Be(1);
		}
	}
}
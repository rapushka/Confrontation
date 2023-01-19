using System.Linq;
using Confrontation.Editor;
using FluentAssertions;
using NUnit.Framework;

namespace Confrontation.Tests
{
	public class FieldTests
	{
		[TearDown] public void TearDown() => Destroy.All<Cell>();

		[Test]
		public void WhenFieldInitialize_AndIsWasEmpty_ThenShouldNotContainNullElements()
		{
			// Arrange.
			var field = Setup.Field();

			// Act.
			field.GenerateField();

			// Assert.
			var cells = field.GetCells().Cast<Cell.Data>();
			cells.All((c) => c is not null).Should().BeTrue();
		}
	}
}
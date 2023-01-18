using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Confrontation.Tests
{
	public class FieldTests
	{
		[Test]
		public void WhenFieldInitialize_AndIsWasEmpty_ThenShouldNotContainNullElements()
		{
			// Arrange.
			var field = Setup.Field();

			// Act.
			field.GenerateField();

			// Assert.
			var cells = field.GetCells().Cast<Cell>();
			cells.All((c) => c is not null).Should().BeTrue();
		}
	}
}
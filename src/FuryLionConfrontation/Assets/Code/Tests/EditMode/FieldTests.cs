using System.Linq;
using Confrontation;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.EditMode
{
	public class FieldTests
	{
		[Test]
		public void WhenFieldInitialize_AndIsWasEmpty_ThenShouldNotContainNullElements()
		{
			// Arrange.
			var field = Setup.Field();

			// Act.
			field.Initialize();

			// Assert.
			var cells = field.GetCells().Cast<Cell>();
			cells.All((c) => c is not null).Should().BeTrue();
		}
	}
}
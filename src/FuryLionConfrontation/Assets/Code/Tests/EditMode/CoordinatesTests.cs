using FluentAssertions;
using NUnit.Framework;

namespace Confrontation.Editor.Tests
{
	public class CoordinatesTests
	{
		[Test]
		public void WhenCalculatePosition_AndRowIs0AndColumnIs0_ThenPositionShouldBeXIs0AndYIs0()
		{
			// Arrange.
			var coordinates = Create.Coordinates(row: 0, column: 0);

			// Act.
			var position = coordinates.CalculatePosition();

			// Assert.
			position.x.Should().Be(0);
			position.y.Should().Be(0);
		}
		
		[Test]
		public void WhenCalculatePosition_AndRowIs1AndColumnIs1_ThenPositionShouldBeXIs1dot5AndYIs0dot866()
		{
			// Arrange.
			var coordinates = Create.Coordinates(row: 1, column: 1);

			// Act.
			var position = coordinates.CalculatePosition();

			// Assert.
			position.x.Should().Be(1.5f);
			position.y.Should().BeGreaterThan(0.8f).And.BeLessThan(0.9f);
		}
	}
}
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

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
			const int row = 1;
			const int column = 1;
			var coordinates = Create.Coordinates(row, column);

			// Act.
			var position = coordinates.CalculatePosition();

			// Assert.
			position.x.Should().Be(column * Constants.HexagonWidth + Constants.HorizontalOffsetForOddRows);
			position.y.Should().Be(Constants.HexagonWidth * 3 / (2 * Mathf.Sqrt(3)));
		}
	}
}
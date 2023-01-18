using System.Linq;
using Confrontation;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

public class Test
{
	[Test]
	public void WhenGeneratedField_AndIsWasEmpty_ThenShouldNotContainNullCells()
	{
		// Arrange.
		var prefab = new GameObject().AddComponent<Cell>();
		var field = new Field(prefab);
		var cells = field.GetCells();

		// Act.
		field.Initialize();

		// Assert.
		var containNullElements = cells.Cast<Cell>().Any((c) => c is null);
		containNullElements.Should().BeFalse();
	}
}
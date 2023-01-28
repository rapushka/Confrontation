using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class FieldGeneratorTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			Container.SetupFieldGenerator();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();

			Destroy.All<Cell>();
			Destroy.All<Village>();
		}

		[Test]
		public void WhenInitialized_AndFieldWasEmpty_ThenFieldShouldNotContainEmptyElements()
		{
			// Arrange.
			var field = Container.Resolve<Field>();
			var generator = Container.Resolve<FieldGenerator>();

			// Act.
			generator.Initialize();

			// Assert.
			var cells = field.Cells;
			cells.Should().AllBeOfType<Cell>();
		}
	}
}
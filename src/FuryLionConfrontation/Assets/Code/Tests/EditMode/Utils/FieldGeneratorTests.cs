using FluentAssertions;
using NUnit.Framework;
using Zenject;

namespace Confrontation.Editor.Tests
{
	[TestFixture]
	public class FieldGeneratorTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void SetUp()
		{
			Container.BindField();
			Container.BindFieldGenerator();
		}

		[TearDown]
		public void TearDown()
		{
			Container.UnbindAll();
		}

		[Test]
		public void WhenInitialized_AndFieldWasEmpty_ThenFieldShouldNotContainEmptyElements()
		{
			// Arrange.
			var field = Container.Resolve<ConfigurableField>();
			var generator = Container.Resolve<FieldGenerator>();

			// Act.
			generator.Initialize();

			// Assert.
			var cells = field.Cells;
			cells.Should().AllBeOfType<Cell>();
		}
	}
}
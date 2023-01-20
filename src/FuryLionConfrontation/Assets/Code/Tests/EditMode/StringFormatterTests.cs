using FluentAssertions;
using NUnit.Framework;
using Confrontation.Editor;

namespace Confrontation.Editor.Tests
{
	public class StringFormatterTests
	{
		[Test]
		public void WhenFormat_AndLowerFirst_ThenUpperFirst()
		{
			// Arrange.
			var name = "width";

			// Act.
			name = name.Format();

			// Assert.
			name.Should().Be("Width");
		}

		[Test]
		public void WhenFormat_AndUnderScoreLowerFirst_ThenUpperFirst()
		{
			// Arrange.
			var name = "_width";

			// Act.
			name = name.Format();

			// Assert.
			name.Should().Be("Width");
		}

		[Test]
		public void WhenFormat_AndCamelCase_ThenAddSpaces()
		{
			// Arrange.
			var name = "someText";

			// Act.
			name = name.Format();

			// Assert.
			name.Should().Be("Some Text");
		}

		[Test]
		public void WhenFormat_AndCamelCaseWithUnderScore_ThenAddSpacesAndFirstToUpper()
		{
			// Arrange.
			var name = "_someAnotherText";

			// Act.
			name = name.Format();

			// Assert.
			name.Should().Be("Some Another Text");
		}
	}
}
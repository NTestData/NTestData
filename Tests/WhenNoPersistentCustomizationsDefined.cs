using FluentAssertions;
using Xunit;

namespace NTestData.Framework.Tests
{
    public class WhenNoPersistentCustomizationsDefined
    {
        private class C
        {
            public int V;
            public string S;
        }

        public WhenNoPersistentCustomizationsDefined()
        {
            TestData.ClearAllPermanentCustomizations();
        }

        [Fact]
        public void CreatedObjectIsNaked()
        {
            var c = TestData.Create<C>();

            c.V.Should().Be(default(int));
            c.S.Should().Be(default(string));
        }

        [Fact]
        public void CreatingObjectWithSingleAdhocCustomizationProducesObjectWithThatCustomizationApplied()
        {
            var c = TestData.Create<C>(x => x.V = int.MinValue);

            c.V.Should().Be(int.MinValue);
        }

        [Fact]
        public void CreatingObjectWithTwoAdhocCustomizationsProducesObjectWithBothCustomizationsApplied()
        {
            var c = TestData.Create<C>(x => x.S = "XYZ", x => x.V = int.MaxValue);

            c.V.Should().Be(int.MaxValue);
            c.S.Should().Be("XYZ");
        }
    }
}

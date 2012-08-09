using FluentAssertions;
using Xunit;

namespace NTestData.Tests
{
    public class WhenPersistentCustomizationDefinedForBaseClass
    {
        private abstract class B
        {
            public int V;
        }

        private class D : B
        {
        }

        public WhenPersistentCustomizationDefinedForBaseClass()
        {
            TestData.ClearAllPermanentCustomizations();
            TestData.SetPermanentCustomizations<B>(b => b.V = int.MaxValue);
        }

        [Fact]
        public void ItIsAppliedToCreatedObjectOfDerivedClass()
        {
            var d = TestData.Create<D>();

            d.V.Should().Be(int.MaxValue);
        }
    }
}

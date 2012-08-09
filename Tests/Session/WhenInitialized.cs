using FluentAssertions;
using Xunit;

namespace NTestData.Tests.Session
{
    public class WhenInitialized
    {
        private readonly TestDataSession _testDataSession;

        public WhenInitialized()
        {
            _testDataSession = new TestDataSession();
        }

        [Fact]
        public void HasActivatorInstantiator()
        {
            _testDataSession.Instantiator.Should().BeOfType<ActivatorInstantiator>();
        }

        [Fact]
        public void HasStandardCustomizationsContainer()
        {
            _testDataSession.Customizations.Should().BeOfType<StandardCustomizationsContainer>();
        }
    }
}

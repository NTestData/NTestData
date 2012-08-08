using FakeItEasy;
using Xunit;

namespace NTestData.Framework.Tests.Session
{
    public class WhenCreatesObject
    {
        private class C
        {
        }

        [Fact]
        public void AsksContainerForApplicableCustomizations()
        {
            var fakeCustomizationsContainer = A.Fake<ICustomizationsContainer>();
            var testDataSession = new TestDataSession
                                      {
                                          Customizations = fakeCustomizationsContainer
                                      };

            testDataSession.Create<C>();

            A.CallTo(() => fakeCustomizationsContainer.GetApplicableToType<C>())
                .MustHaveHappened();
        }
    }
}

using FakeItEasy;
using Xunit;

namespace NTestData.Tests.Session
{
    public class WhenCreatesObjectWithNoDefaultConstructor
    {
        private class C
        {
            public C(int x)
            {
            }
        }

        private C CFactory()
        {
            return new C(default(int));
        }

        [Fact]
        public void AsksContainerForApplicableCustomizations()
        {
            var fakeCustomizationsContainer = A.Fake<ICustomizationsContainer>();
            var testDataSession = new TestDataSession
                {
                    Customizations = fakeCustomizationsContainer
                };

            testDataSession.Create<C>(CFactory);

            A.CallTo(() => fakeCustomizationsContainer.GetApplicableToType<C>())
                .MustHaveHappened();
        }
    }
}

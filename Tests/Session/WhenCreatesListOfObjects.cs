using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace NTestData.Tests.Session
{
    public class WhenCreatesListOfObjects
    {
        private readonly TestDataSession _testDataSession = new TestDataSession();
        
        private const ushort NumberOfObjectsToCreate = 7;

        private class C
        {
            public int V;
        }

        [Fact]
        public void ResultingListIsOfRequestedSize()
        {
            var list = _testDataSession.CreateListOf<C>(NumberOfObjectsToCreate);

            list.Should().HaveCount(NumberOfObjectsToCreate);
        }

        [Fact]
        public void EachItemInResultingListIsInstantiatedToDefault()
        {
            var list = _testDataSession.CreateListOf<C>(NumberOfObjectsToCreate);

            list.ToList().ForEach(c => c.V.Should().Be(default(int)));
        }

        [Fact]
        public void AsksContainerForApplicableCustomizationsForEachCreatedItem()
        {
            var fakeCustomizationsContainer = A.Fake<ICustomizationsContainer>();
            _testDataSession.Customizations = fakeCustomizationsContainer;

            _testDataSession.CreateListOf<C>(NumberOfObjectsToCreate);

            A.CallTo(() => fakeCustomizationsContainer.GetApplicableToType<C>())
                .MustHaveHappened(Repeated.Exactly.Times(NumberOfObjectsToCreate));
        }
    }
}

using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NTestData.Framework.Tests.Container
{
    public class WhenDuplicateCustomizationsAreNotAllowed
    {
        private readonly StandardCustomizationsContainer _container
            = new StandardCustomizationsContainer(allowDuplicates: false);

        private class C
        {
            public int M;
        }

        private static readonly Action<C> IncreaseMemberBySeven = c => c.M += 7;

        public WhenDuplicateCustomizationsAreNotAllowed()
        {
            _container.AddForType(IncreaseMemberBySeven);
            _container.AddForType(IncreaseMemberBySeven);
            _container.AddForType(IncreaseMemberBySeven);
        }

        [Fact]
        public void CorrespondingPropertyIsFalse()
        {
            _container.AllowDuplicates.Should().BeFalse();
        }

        [Fact]
        public void NoDuplicateCustomizationsAreReturned()
        {
            var actual = new C();
            var expected = new C();
            IncreaseMemberBySeven(expected);

            var customizations = _container.GetApplicableToType<C>().ToList();

            customizations.Should().HaveCount(1);

            customizations.ForEach(act => act(actual));
            actual.M.Should().Be(expected.M);
        }
    }
}

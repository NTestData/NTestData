using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NTestData.Framework.Tests.Container
{
    public class WhenDuplicateCustomizationsAreAllowed
    {
        private readonly StandardCustomizationsContainer _container
            = new StandardCustomizationsContainer(allowDuplicates: true);

        private class C
        {
            public int M;
        }

        private static readonly Action<C> IncreaseMemberBySeven = c => c.M += 7;

        public WhenDuplicateCustomizationsAreAllowed()
        {
            _container.AddForType(IncreaseMemberBySeven);
            _container.AddForType(IncreaseMemberBySeven);
            _container.AddForType(IncreaseMemberBySeven);
        }

        [Fact]
        public void CorrespondingPropertyIsTrue()
        {
            _container.AllowDuplicates.Should().BeTrue();
        }

        [Fact]
        public void AllDuplicateCustomizationsAreReturned()
        {
            var c = new C();

            var customizations = _container.GetApplicableToType<C>().ToList();
            customizations.ForEach(act => act(c));

            customizations.Should().HaveCount(1);
            c.M.Should().Be(21);

            var invocations = customizations.SelectMany(a => a.GetInvocationList()).ToArray();
            invocations.Should().HaveCount(3);
            invocations.Should().OnlyContain(func => func.Equals(IncreaseMemberBySeven));
        }
    }
}

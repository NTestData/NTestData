using System;
using FluentAssertions;
using Xunit;

namespace NTestData.Tests.Container
{
    public class WhenThereAreAddedCustomizationsForType
    {
        private readonly StandardCustomizationsContainer _container = new StandardCustomizationsContainer();

        private abstract class C
        {
            public int M;
        }

        private static readonly Action<C> DecrementMember = c => c.M -= 1;
        private static readonly Action<C> IncrementMember = c => c.M += 1;
        private static readonly Action<C> NegateMember = c => c.M *= -1;

        public WhenThereAreAddedCustomizationsForType()
        {
            _container.AddForType(DecrementMember);
            _container.AddForType(IncrementMember);
            _container.AddForType(NegateMember);
        }

        [Fact]
        public void AsSoonAsCustomizationsForThatTypeAreClearedThereShouldBeNoneForThisType()
        {
            _container.ClearForType<C>();

            _container.GetApplicableToType<C>().Should().BeEmpty();
        }
    }
}

using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NTestData.Tests.Container
{
    public class WhenApplicableCustomizationsAreNotScopedToRequestedTypeOnly
    {
        private readonly StandardCustomizationsContainer _container = new StandardCustomizationsContainer();

        private abstract class B
        {
            public string S;
        }

        private interface I
        {
            int N { get; set; }
        }

        private class C : B, I
        {
            public int N { get; set; }
        }

        private static readonly Action<B> BaseTypeCustomization = b => b.S = "foo";
        private static readonly Action<C> ConcreteTypeCustomization = c => c.S = "bar";
        private static readonly Action<I> ImplementedInterfaceCustomization = i => i.N = int.MaxValue;

        public WhenApplicableCustomizationsAreNotScopedToRequestedTypeOnly()
        {
            _container.AddForType(BaseTypeCustomization);
            _container.AddForType(ConcreteTypeCustomization);
            _container.AddForType(ImplementedInterfaceCustomization);

            _container.ScopeApplicableCustomizationsToRequestedTypeOnly = false;
        }

        [Fact]
        public void CorrespondingPropertyIsFalse()
        {
            _container.ScopeApplicableCustomizationsToRequestedTypeOnly.Should().BeFalse();
        }

        [Fact]
        public void CustomizationsForRequestedTypeAreApplied()
        {
            var applicableCustomizations = _container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().HaveCount(3);
            applicableCustomizations.Should().Contain(ConcreteTypeCustomization);
        }

        [Fact]
        public void CustomizationsForBaseClassAreApplied()
        {
            var applicableCustomizations = _container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().Contain(BaseTypeCustomization);
        }

        [Fact]
        public void CustomizationsForImplementedInterfacesAreApplied()
        {
            var applicableCustomizations = _container.GetApplicableToType<C>().ToArray();

            applicableCustomizations.Should().Contain(ImplementedInterfaceCustomization);
        }
    }
}

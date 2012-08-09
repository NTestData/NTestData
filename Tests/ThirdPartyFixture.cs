using AutoPoco;
using FizzWare.NBuilder;
using Xunit;

namespace NTestData.Tests
{
    public class ThirdPartyFixture
    {
        private class D
        {
        }

        private class C
        {
            public int X { get; private set; }
            public D DRef { get; private set; }

            public C(int x, D dRef)
            {
                X = x;
                DRef = dRef;
            }
        }

        [Fact]
        public void NBuilderCannotInstantiateTypesWithoutParameterlessCtor()
        {
            var exception = Record.Exception(() => Builder<C>.CreateNew().Build());

            Assert.IsType<TypeCreationException>(exception);
        }

        [Fact]
        public void AutoPocoCanInstantiateTypesWithoutParameterlessCtor()
        {
            var autoPocoSession = AutoPocoContainer.CreateDefaultSession();

            var c = autoPocoSession.Single<C>().Get();

            Assert.Equal(default(int), c.X);
            Assert.NotNull(c.DRef);
        }
    }
}

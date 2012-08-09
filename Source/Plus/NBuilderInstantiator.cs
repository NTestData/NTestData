namespace NTestData.Plus
{
    using FizzWare.NBuilder;

    public class NBuilderInstantiator : IInstantiator
    {
        T IInstantiator.Instantiate<T>()
        {
            var instance = Builder<T>.CreateNew().Build();

            return instance;
        }
    }
}

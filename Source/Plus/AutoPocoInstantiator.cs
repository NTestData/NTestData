namespace NTestData.Plus
{
    using AutoPoco;
    using AutoPoco.Engine;

    public class AutoPocoInstantiator : IInstantiator
    {
        public IGenerationSession GenerationSession { get; set; }

        public AutoPocoInstantiator()
        {
            GenerationSession = AutoPocoContainer.CreateDefaultSession();
        }

        T IInstantiator.Instantiate<T>()
        {
            return GenerationSession.Single<T>().Get();
        }
    }
}

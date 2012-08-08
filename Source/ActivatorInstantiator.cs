namespace NTestData.Framework
{
    public class ActivatorInstantiator : IInstantiator
    {
        T IInstantiator.Instantiate<T>()
        {
            return (T) System.Activator.CreateInstance(typeof (T));
        }
    }
}

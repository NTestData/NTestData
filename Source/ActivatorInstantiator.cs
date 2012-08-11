namespace NTestData
{
    /// <summary>
    /// Instantiates objects of specified types using <see cref="System.Activator"/>.
    /// </summary>
    public class ActivatorInstantiator : IInstantiator
    {
        /// <summary>
        /// Creates instance of specified type <typeparamref name="T"/>
        /// using <see cref="System.Activator.CreateInstance(System.Type)"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to be instantiated.</typeparam>
        /// <returns>Instance of specified type <typeparamref name="T"/>.</returns>
        T IInstantiator.Instantiate<T>()
        {
            return (T) System.Activator.CreateInstance(typeof (T));
        }
    }
}

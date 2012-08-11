namespace NTestData
{
    /// <summary>
    /// Instantiates objects of specified types.
    /// </summary>
    public interface IInstantiator
    {
        /// <summary>
        /// Creates instance of specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to be instantiated.</typeparam>
        /// <returns>Instance of specified type <typeparamref name="T"/>.</returns>
        T Instantiate<T>();
    }
}

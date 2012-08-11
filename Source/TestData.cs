namespace NTestData
{
    using System;
    using System.Collections.Generic;
#if PLUS
    using Plus;
#endif

    public static class TestData
    {
        static TestData()
        {
            Session = new TestDataSession(
#if PLUS && AUTOPOCO
                new AutoPocoInstantiator()
#elif PLUS && NBUILDER
                new NBuilderInstantiator()
#endif
                );
        }

        public static TestDataSession Session { get; set; }

        /// <summary>
        /// Clears all previously stored permanent customizations for objects of all types.
        /// </summary>
        public static void ClearAllPermanentCustomizations()
        {
            Session.Customizations.ClearAll();
        }

        /// <summary>
        /// Clears all permanent customizations for objects of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>Permament customizations for derived types remain intact.</remarks>
        public static void ClearPermanentCustomizations<T>()
        {
            Session.Customizations.ClearForType<T>();
        }

        /// <summary>
        /// Creates object of specified type <typeparamref name="T"/>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="customizations">Ad-hoc customizations to be applied to resulting object.</param>
        /// <returns>
        /// Object of type <typeparamref name="T"/>
        /// with all type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </returns>
        public static T Create<T>(params Action<T>[] customizations) where T : class, new()
        {
            return Session.Create(customizations);
        }

        /// <summary>
        /// Creates list of objects of specified type <typeparamref name="T"/>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="size">List capacity (i.e. number of objects in list)</param>
        /// <returns>
        /// List of objects of specified type <typeparamref name="T"/>
        /// with all the type-applicable permanent customizations applied
        /// as well as passed ad-hoc ones.
        /// </returns>
        public static IList<T> CreateListOf<T>(ushort size) where T : class, new()
        {
            return Session.CreateListOf<T>(size);
        }

        /// <summary>
        /// Stores customizations permanently and then applies them
        /// to every created object of type <typeparamref name="T"/> or of derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="customizations">
        /// Permanent customizations to be stored and then applied on-the-fly
        /// to all resulting objects of type <typeparamref name="T"/>
        /// or all resulting objects of derived types of <typeparamref name="T"/>.
        /// </param>
        public static void SetPermanentCustomizations<T>(params Action<T>[] customizations) where T : class
        {
            Session.Customizations.AddForType(customizations);
        }
    }
}

namespace NTestData.Framework
{
    using System;
    using System.Collections.Generic;

    public class TestDataSession
    {
        protected TestDataSession(
            IInstantiator instantiator,
            ICustomizationsContainer customizations)
        {
            Instantiator = instantiator;
            Customizations = customizations;
        }

        public TestDataSession()
            : this(new ActivatorInstantiator(),
                   new StandardCustomizationsContainer()) { }

        public IInstantiator Instantiator { get; set; }

        public ICustomizationsContainer Customizations { get; set; }

        /// <summary>
        /// Creates object of specified type <typeparam name="T">T</typeparam>
        /// with all the type-applicable customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="customizations">Ad-hoc customizations to be applied to resulting object.</param>
        /// <returns>
        /// Customized object of type <typeparam name="T">T</typeparam>.
        /// </returns>
        public T Create<T>(params Action<T>[] customizations)
        {
            var instance = Instantiator.Instantiate<T>();

            return Create(() => instance, customizations);
        }

        public T Create<T>(Func<T> instantiator, params Action<T>[] customizations)
        {
            var obj = instantiator();

            if (Customizations != null)
            {
                var permanentCustomizations = Customizations.GetApplicableToType<T>();
                obj.Customize(permanentCustomizations);
            }

            obj.Customize(customizations);

            return obj;
        }

        /// <summary>
        /// Creates list of objects of specified type <typeparam name="T">T</typeparam>
        /// with all the type-applicable customizations applied
        /// as well as passed ad-hoc ones.
        /// </summary>
        /// <typeparam name="T">Type of object to be created.</typeparam>
        /// <param name="size">List capacity (i.e. number of objects in list)</param>
        /// <returns>
        /// List of customized objects of specified type <typeparam name="T">T</typeparam>.
        /// </returns>
        public IList<T> CreateListOf<T>(ushort size)
        {
            var result = new List<T>(size);
            for (int i = 0; i < size; i++)
            {
                result.Add(Create<T>());
            }
            return result;
        }
    }
}

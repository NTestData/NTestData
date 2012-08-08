namespace NTestData.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CustomizationExtensions
    {
        public static T Customize<T>(this T obj, params Action<T>[] customizations)
        {
            foreach (var action in customizations)
            {
                action(obj);
            }

            return obj;
        }

        public static T Customize<T>(this T obj, IEnumerable<Action<T>> customizations)
        {
            return Customize(obj, (customizations ?? Enumerable.Empty<Action<T>>()).ToArray());
        }
    }
}

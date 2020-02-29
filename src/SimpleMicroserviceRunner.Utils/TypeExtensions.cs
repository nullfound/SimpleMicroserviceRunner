using System;

namespace SimpleMicroserviceRunner.Utils
{
    public static class TypeExtensions
    {
        public static bool Implements<T>(this Type type)
            where T : class
        {
            return typeof(T).IsAssignableFrom(type) && type != typeof(T) && !type.IsAbstract;
        }
    }
}

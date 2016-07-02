using System;
using System.Linq;

namespace Chassis.Types
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Returns true if the supplied <paramref name="type"/> implements the given interface <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type (interface) to check for.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the given type implements the specified interface.</returns>
        /// <remarks>This method is for interfaces only. Use <seealso cref="Inherits"/> for class types and <seealso cref="InheritsOrImplements"/> 
        /// to check both interfaces and classes.</remarks>
        public static bool Implements<T>(this Type type)
        {
            return type.Implements(typeof(T));
        }

        /// <summary>
        /// Returns true of the supplied <paramref name="type"/> implements the given interface <paramref name="interfaceType"/>. If the given
        /// interface type is a generic type definition this method will use the generic type definition of any implemented interfaces
        /// to determine the result.
        /// </summary>
        /// <param name="interfaceType">The interface type to check for.</param>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the given type implements the specified interface.</returns>
        /// <remarks>This method is for interfaces only. Use <seealso cref="Inherits"/> for classes and <seealso cref="InheritsOrImplements"/> 
        /// to check both interfaces and classes.</remarks>
        public static bool Implements(this Type type, Type interfaceType)
        {
            if (type == null || interfaceType == null || type == interfaceType)
                return false;
            if (interfaceType.IsGenericTypeDefinition && type.GetInterfaces().Where(t => t.IsGenericType).Select(t => t.GetGenericTypeDefinition()).Any(gt => gt == interfaceType))
            {
                return true;
            }
            return interfaceType.IsAssignableFrom(type);
        }
    }
}
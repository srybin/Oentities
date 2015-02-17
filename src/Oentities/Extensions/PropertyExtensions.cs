using System;
using System.Collections.Generic;
using System.Linq;

namespace Oentities.Extensions
{
    static class PropertyExtensions
    {
        public static bool IsPrimitiveType(this Type type)
        {
            if (type == typeof(char))
                return false;

            if (type.IsPrimitive)
                return true;

            if (type == typeof(string))
                return true;

            if (type == typeof(DateTime))
                return true;

            if (type == typeof(TimeSpan))
                return true;

            if (type == typeof(Guid))
                return true;

            if (type == typeof(decimal))
                return true;

            if (type == typeof(byte[]))
                return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return IsPrimitiveType(Nullable.GetUnderlyingType(type));

            return false;
        }

        public static bool IsGuid(this Type type)
        {
            return type == typeof(Guid);
        }

        public static bool IsCollectionType(this Type type)
        {
            IEnumerable<Type> interfaces = type.GetInterfaces();
            if (type.IsInterface) interfaces = interfaces.Concat(new[] { type });
            
            return interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        public static Type GetEntityType(this Type type)
        {
            return type.GetInterfaces()
                       .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                       .GetGenericArguments()
                       .First();
        }
    }
}
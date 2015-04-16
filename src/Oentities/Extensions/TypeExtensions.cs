using System;
using System.Linq.Expressions;

namespace Oentities.Extensions
{
    static class TypeExtensions
    {
        public static Expression<Func<object, object>> CreateGetPropertyExpression(this Type entity, string property)
        {
            var objectType = typeof(object);
            var param = Expression.Parameter(objectType, "e");
            var body = Expression.Property(Expression.TypeAs(param, entity), property);
            return Expression.Lambda<Func<object, object>>(Expression.TypeAs(body, objectType), param);
        }
    }
}
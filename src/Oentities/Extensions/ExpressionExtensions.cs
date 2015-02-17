using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Oentities.Extensions
{
    static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfoBy<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> lambda)
        {
            MemberExpression expression = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    expression = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                    break;

                case ExpressionType.MemberAccess:
                    expression = lambda.Body as MemberExpression;
                    break;
            }

            if (expression == null || !(expression.Member is PropertyInfo))
            {
                var message = string.Format("Can not get property name from the following lambda: {0}", lambda);
                throw new ArgumentException(message, "lambda");
            }

            return (PropertyInfo)expression.Member;
        }
    }
}
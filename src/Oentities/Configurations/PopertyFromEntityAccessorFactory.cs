using Oentities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oentities.Configurations
{
    class PopertyFromEntityAccessorFactory : IPopertyFromEntityAccessorFactory
    {
        public IPropertyFromEntityAccessor Create(IEnumerable<KeyValuePair<Type, IEntityConfiguration>> configurations)
        {
            var funcs = new Dictionary<Type, IDictionary<string, Func<object, object>>>();

            foreach (var c in configurations)
            {
                funcs.Add(c.Key, c.Value.Properties.Where(p => p.Info != null)
                    .ToDictionary(p => p.Info.Name, p => c.Key.CreateGetPropertyExpression(p.Info.Name).Compile())
                );
            }

            return new PropertyFromEntityAccessor(funcs);
        }
    }
}
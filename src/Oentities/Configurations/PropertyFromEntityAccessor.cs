using System;
using System.Collections.Generic;

namespace Oentities.Configurations
{
    class PropertyFromEntityAccessor : IPropertyFromEntityAccessor
    {
        private readonly IDictionary<Type, IDictionary<string, Func<object, object>>> _funcs;
 
        public PropertyFromEntityAccessor(IDictionary<Type, IDictionary<string, Func<object, object>>> funcs)
        {
            _funcs = funcs;
        }

        public object Get(object entity, Property property)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            if (property.Info == null)
                throw new InvalidOperationException("property.Info must exist.");

            return _funcs[property.EntityType][property.Info.Name](entity);
        }
    }
}
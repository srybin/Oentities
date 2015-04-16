using System;
using System.Collections.Generic;

namespace Oentities.Configurations
{
    interface IPopertyFromEntityAccessorFactory
    {
        IPropertyFromEntityAccessor Create(IEnumerable<KeyValuePair<Type, IEntityConfiguration>> configurations);
    }
}
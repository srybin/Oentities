using System;
using System.Collections.Generic;
using System.Reflection;

namespace Oentities.Configurations
{
    public interface IEntityConfiguration
    {
        Type EntityType { get; }

        string TableName { get; }
        
        PropertyInfo Key { get; }
        
        ICollection<Property> Properties { get; }
    }
}
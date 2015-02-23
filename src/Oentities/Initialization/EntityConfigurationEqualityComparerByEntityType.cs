using System.Collections.Generic;
using Oentities.Configurations;

namespace Oentities.Initialization
{
    public class EntityConfigurationEqualityComparerByEntityType : IEqualityComparer<IEntityConfiguration>
    {
        public bool Equals(IEntityConfiguration x, IEntityConfiguration y)
        {
            return x.EntityType == y.EntityType;
        }

        public int GetHashCode(IEntityConfiguration obj)
        {
            return obj.EntityType.GetHashCode();
        }
    }
}
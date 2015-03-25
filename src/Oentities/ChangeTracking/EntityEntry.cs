using System.Collections.Generic;

namespace Oentities.ChangeTracking
{
    public enum EntityState
    {
        Clean,
        New,
        Dirty,
        Deleted
    }

    public class EntityEntry
    {
        public object Entity { get; set; }
        public EntityState State { get; set; }
        public IDictionary<string, object> ForeignProperties { get; set; } 
    }
}
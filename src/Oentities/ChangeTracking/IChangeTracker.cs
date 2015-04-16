using System.Collections.Generic;

namespace Oentities.ChangeTracking
{
    public interface IChangeTracker
    {
        void Attach(object entity, EntityState state);

        EntityEntry Entry(object entity);

        IReadOnlyDictionary<object, EntityEntry> Entries { get; } 

        bool HasChanges();
    }
}
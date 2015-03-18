using System.Collections.Generic;
using System.Linq;

namespace Oentities.ChangeTracking
{
    public class ChangeTracker
    {
        private readonly IDictionary<object, EntityEntry> _identityMap = new Dictionary<object, EntityEntry>();

        public void Attach(object entity, EntityState state)
        {
            if (!_identityMap.ContainsKey(entity))
            {
                _identityMap.Add(entity, new EntityEntry
                {
                    Entity = entity, ForforeignProperties = new Dictionary<string, object>()
                });
            }

            _identityMap[entity].State = state;
        }

        public EntityEntry Entry(object entity)
        {
            return _identityMap.ContainsKey(entity) ? _identityMap[entity] : null;
        }

        public bool HasChanges()
        {
            return _identityMap.Count != 0 && _identityMap.Values.Any(e => e.State != EntityState.Clean);
        }
    }
}
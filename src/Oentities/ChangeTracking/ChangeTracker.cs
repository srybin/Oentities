using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Oentities.ChangeTracking
{
    public class ChangeTracker : IChangeTracker
    {
        private readonly IDictionary<object, EntityEntry> _identityMap = new Dictionary<object, EntityEntry>();

        public void Attach(object entity, EntityState state)
        {
            if (!_identityMap.ContainsKey(entity))
            {
                _identityMap.Add(entity, new EntityEntry
                {
                    Entity = entity, ExternalLinks = new Dictionary<string, object>()
                });
            }

            _identityMap[entity].State = state;
        }

        public EntityEntry Entry(object entity)
        {
            return _identityMap.ContainsKey(entity) ? _identityMap[entity] : null;
        }

        public IReadOnlyDictionary<object, EntityEntry> Entries
        {
            get { return new ReadOnlyDictionary<object, EntityEntry>(_identityMap); }
        }

        public bool HasChanges()
        {
            return _identityMap.Count != 0 && _identityMap.Values.Any(e => e.State != EntityState.Clean);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Oentities.Configurations;

namespace Oentities.ChangeTracking
{
    class EntityEntrySetter : IEntityEntrySetter
    {
        private readonly IChangeTracker _changeTracker;
        private readonly IDictionary<Type, IEntityConfiguration> _configurations;
        private readonly IPropertyFromEntityAccessor _propertyFromEntityAccessor;

        public EntityEntrySetter(
            IChangeTracker changeTracker,
            IDictionary<Type, IEntityConfiguration> configurations,
            IPropertyFromEntityAccessor propertyFromEntityAccessor)
        {
            _changeTracker = changeTracker;
            _configurations = configurations;
            _propertyFromEntityAccessor = propertyFromEntityAccessor;
        }

        public void ForEachEntitiesFromCollectionEntitySetHer()
        {
            var properties = _configurations.Values.SelectMany(c => c.Properties)
                .Where(p => p is OneToManyWithoutInversePropertyRelationshipProperty);

            foreach (var p in properties.OfType<RelationshipProperty>())
                foreach (var e in _changeTracker.Entries.Keys.Where(e => e.GetType() == p.EntityType).ToList())
                    ForEachEntitiesFromCollectionEntitySetThis(e, p);
        }

        public void ForEachEntitiesFromCollectionEntitySetThis(object entity, RelationshipProperty property)
        {
            var collection = _propertyFromEntityAccessor.Get(entity, property) as IEnumerable<object>;
            if (collection == null) return;

            foreach (var e in collection.Where(e => _changeTracker.Entries.ContainsKey(e)))
                _changeTracker.Entries[e].ExternalLinks[string.Concat(property.EntityType.Name, "_Id")] = entity;
        }

        public void FindAndSetInverseSideFor(object entity, RelationshipProperty property)
        {
            foreach (var e in _changeTracker.Entries.Keys.Where(e => e.GetType() == property.InverseProperty.EntityType))
            {
                var collection = _propertyFromEntityAccessor.Get(e, property.InverseProperty) as IEnumerable<object>;
                if (collection == null) continue;

                if (collection.Contains(entity) && _changeTracker.Entries.ContainsKey(entity))
                {
                    _changeTracker.Entries[entity].ExternalLinks[string.Concat(property.InverseProperty.EntityType.Name, "_Id")] = e;
                }
            }
        }
    }
}
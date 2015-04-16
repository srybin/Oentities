using System;
using System.Collections.Generic;
using System.Linq;
using Oentities.ChangeTracking;
using Oentities.Configurations;

namespace Oentities
{
    public abstract class UnitOfWork
    {
        private static IDictionary<Type, IEntityConfiguration> _entityConfigurations;
        private static IPropertyFromEntityAccessor _propertyFromEntityAccessor;
        private static readonly object _lock = new object();

        private readonly IModelBuilderWithSetAllNullInverseReference _modelBuilder;
        private readonly IEntityEntrySetter _entityEntrySetter;
        private readonly ChangeTracker _changeTracker;

        protected UnitOfWork()
        {
            _changeTracker = new ChangeTracker();
            _modelBuilder = new ModelBuilder();

            if (_entityConfigurations == null)
            {
                lock (_lock)
                {
                    if (_entityConfigurations == null)
                    {
                        ModelInit(_modelBuilder);
                        _modelBuilder.SetAllNullInverseReferenceProperties();
                        _entityConfigurations = _modelBuilder.Configurations.ToDictionary(c => c.EntityType);
                        _propertyFromEntityAccessor = new PopertyFromEntityAccessorFactory().Create(_entityConfigurations);
                    }
                }
            }

            _entityEntrySetter = new EntityEntrySetter(_changeTracker, _entityConfigurations, _propertyFromEntityAccessor);
        }

        public IReadOnlyDictionary<object, EntityEntry> Entries { get { return _changeTracker.Entries; }}

        public void MarkNew(object entity)
        {
            _changeTracker.Attach(entity, EntityState.New);
        }

        public void MarkDirty(object entity)
        {
            _changeTracker.Attach(entity, EntityState.Dirty);
        }

        public void MarkDeleted(object entity)
        {
            _changeTracker.Attach(entity, EntityState.Deleted);
        }

        public void MarkClean(object entity)
        {
            _changeTracker.Attach(entity, EntityState.Clean);

            var properties = _entityConfigurations[entity.GetType()].Properties.OfType<RelationshipProperty>().ToList();

            foreach (var p in properties.OfType<OneToManyWithoutInversePropertyRelationshipProperty>())
                _entityEntrySetter.ForEachEntitiesFromCollectionEntitySetThis(entity, p);
            
            foreach (var p in properties.Where(p => p is ManyToOneWithInversePropertyRelationshipProperty && p.Info == null))
                _entityEntrySetter.FindAndSetInverseSideFor(entity, p);
        }

        public void Commit()
        {
            _entityEntrySetter.ForEachEntitiesFromCollectionEntitySetHer();
        }

        protected abstract void ModelInit(IModelBuilder modelBuilder);
    }
}
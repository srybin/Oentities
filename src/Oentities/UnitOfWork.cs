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
        private readonly IModelBuilderWithSetAllNullInverseReference _modelBuilder;
        private readonly ChangeTracker _changeTracker;

        protected UnitOfWork()
        {
            _changeTracker = new ChangeTracker();
            _modelBuilder = new ModelBuilder();

            if (_entityConfigurations != null) return;
            ModelInit(_modelBuilder);
            _modelBuilder.SetAllNullInverseReferenceProperties();
            _entityConfigurations = _modelBuilder.Configurations.ToDictionary(c => c.EntityType);
        }

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
        }

        protected abstract void ModelInit(IModelBuilder modelBuilder);
    }
}
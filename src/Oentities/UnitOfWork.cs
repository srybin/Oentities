using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oentities.ChangeTracking;
using Oentities.Configurations;
using Oentities.Initialization;

namespace Oentities
{
    public class UnitOfWork
    {
        private static IModelInitializer _modelInitializer;
        private static IDictionary<Type, IEntityConfiguration> _entityConfigurations;
        private readonly ChangeTracker _changeTracker;

        public UnitOfWork()
        {
            _changeTracker = new ChangeTracker();

            _modelInitializer =
                new ModelInitializerWithSetAllNullInverseReferencePropertiesDecorator(
                    new ModelInitializerWithOnlyUniqueTypesDecorator(new DefaultModelInitializer()));
        }

        public static void InitModelConfigurations(Assembly assembly)
        {
            _entityConfigurations = _modelInitializer.InitModelConfigurations(assembly).ToDictionary(c => c.EntityType);
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
    }
}
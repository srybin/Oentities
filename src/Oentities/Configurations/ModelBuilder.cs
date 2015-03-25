using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Oentities.Configurations
{
    public class ModelBuilder : IModelBuilder, IModelBuilderWithSetAllNullInverseReference
    {
        private readonly ICollection<IEntityConfiguration> _configurations = new Collection<IEntityConfiguration>();

        public IReadOnlyCollection<IEntityConfiguration> Configurations { get { return _configurations.ToList(); } }

        public IModelBuilder BuildFrom(Assembly assembly)
        {
            var type = typeof(IEntityConfiguration);

            var eConfigs = assembly.GetTypes()
                .Where(type.IsAssignableFrom)
                .Select(t => (IEntityConfiguration)Activator.CreateInstance(t))
                .ToList();

            foreach (var eConfig in eConfigs)
            {
                ThrowInvalidOperationExceptionIfModelExistThis(eConfig);
                _configurations.Add(eConfig);
            }

            return this;
        }

        public IModelBuilder Add(IEntityConfiguration configuration)
        {
            ThrowInvalidOperationExceptionIfModelExistThis(configuration);
            _configurations.Add(configuration);
            return this;
        }

        public IModelBuilder Ignore<TEntityConfiguration>() where TEntityConfiguration : IEntityConfiguration
        {
            var type = typeof (TEntityConfiguration);
            var configuration = _configurations.FirstOrDefault(c => c.GetType() == type);

            if (configuration != null)
                _configurations.Remove(configuration);

            return this;
        }

        public void SetAllNullInverseReferenceProperties()
        {
            var properties = _configurations.SelectMany(c => c.Properties)
                .Where(p => p is OneToManyWithoutInversPropertyRelationshipProperty ||
                            p is ManyToManyWithoutInversPropertyRelationshipProperty);

            foreach (var p in properties.OfType<RelationshipProperty>())
            {
                var eConfig = _configurations.First(c => c.EntityType == p.InversProperty.EntityType);
                eConfig.Properties.Add(p.InversProperty);
            }
        }

        private void ThrowInvalidOperationExceptionIfModelExistThis(IEntityConfiguration configuration)
        {
            if (_configurations.Any(c => c.EntityType == configuration.EntityType))
                throw new InvalidOperationException("Model configuration must contain only unique types.");
        }
    }
}
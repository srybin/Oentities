using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oentities.Configurations;

namespace Oentities.Initialization
{
    public class ModelInitializerWithOnlyUniqueTypesDecorator : IModelInitializer
    {
        private readonly IModelInitializer _modelInitializer;

        public ModelInitializerWithOnlyUniqueTypesDecorator(IModelInitializer modelInitializer)
        {
            _modelInitializer = modelInitializer;
        }

        public IReadOnlyCollection<IEntityConfiguration> InitModelConfigurations(Assembly assembly)
        {
            var eConfigs = _modelInitializer.InitModelConfigurations(assembly);
            var eConfigUnique = new HashSet<IEntityConfiguration>(new EntityConfigurationEqualityComparerByEntityType());

            foreach (var eConfig in eConfigs)
            {
                if (eConfigUnique.Contains(eConfig))
                    throw new InvalidOperationException("Model configuration must contain only unique types.");

                eConfigUnique.Add(eConfig);
            }

            return eConfigUnique.ToList();
        }
    }
}
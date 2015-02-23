using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oentities.Configurations;

namespace Oentities.Initialization
{
    public class DefaultModelInitializer : IModelInitializer
    {
        public IReadOnlyCollection<IEntityConfiguration> InitModelConfigurations(Assembly assembly)
        {
            var type = typeof (IEntityConfiguration);

            return assembly.GetTypes()
                .Where(type.IsAssignableFrom)
                .Select(t => (IEntityConfiguration) Activator.CreateInstance(t))
                .ToList();
        }
    }
}
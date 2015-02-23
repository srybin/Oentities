using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oentities.Configurations;
using Oentities.Initialization;

namespace Oentities
{
    public class UnitOfWork
    {
        private static IModelInitializer _modelInitializer;
        private static IDictionary<Type, IEntityConfiguration> _entityConfigurations;

        public UnitOfWork(IModelInitializer modelInitializer)
        {
            _modelInitializer = modelInitializer;
        }

        public static void InitModelConfigurations(Assembly assembly)
        {
            _entityConfigurations = _modelInitializer.InitModelConfigurations(assembly).ToDictionary(c => c.EntityType);
        }
    }
}
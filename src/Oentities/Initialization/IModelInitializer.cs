using System.Collections.Generic;
using System.Reflection;
using Oentities.Configurations;

namespace Oentities.Initialization
{
    public interface IModelInitializer
    {
        IReadOnlyCollection<IEntityConfiguration> InitModelConfigurations(Assembly assembly);
    }
}
using System.Collections.Generic;
using System.Reflection;

namespace Oentities.Configurations
{
    public interface IModelBuilder
    {
        IModelBuilder BuildFrom(Assembly assembly);

        IModelBuilder Add(IEntityConfiguration configuration);

        IModelBuilder Ignore<TEntityConfiguration>() where TEntityConfiguration : IEntityConfiguration;

        IReadOnlyCollection<IEntityConfiguration> Configurations { get; }
    }
}
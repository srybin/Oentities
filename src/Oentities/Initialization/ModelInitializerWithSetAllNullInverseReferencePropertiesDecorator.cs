using Oentities.Configurations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Oentities.Initialization
{
    public class ModelInitializerWithSetAllNullInverseReferencePropertiesDecorator : IModelInitializer
    {
        private readonly IModelInitializer _modelInitializer;

        public ModelInitializerWithSetAllNullInverseReferencePropertiesDecorator(IModelInitializer modelInitializer)
        {
            _modelInitializer = modelInitializer;
        }

        public IReadOnlyCollection<IEntityConfiguration> InitModelConfigurations(Assembly assembly)
        {
            var eConfigs = _modelInitializer.InitModelConfigurations(assembly);

            var properties = eConfigs.SelectMany(c => c.Properties)
                .Where(p => p is OneToManyWithoutInversPropertyRelationshipProperty || p is ManyToManyWithoutInversPropertyRelationshipProperty);

            foreach (var p in properties.OfType<RelationshipProperty>())
            {
                var eConfig = eConfigs.First(c => c.EntityType == p.InversProperty.EntityType);
                eConfig.Properties.Add(p.InversProperty);
            }

            return eConfigs;
        }
    }
}
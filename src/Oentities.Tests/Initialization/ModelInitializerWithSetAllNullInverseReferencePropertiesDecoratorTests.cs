using Moq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Initialization;
using Oentities.Tests.EntityConfigurations;
using System.Linq;

namespace Oentities.Tests.Initialization
{
    [TestFixture]
    public class ModelInitializerWithSetAllNullInverseReferencePropertiesDecoratorTests
    {
        [Test]
        public void ForAllOneToManyWithoutInversPropertyConfigurationSetInversPropertyConfiguration()
        {
            var assembly = GetType().Assembly;
            var mock = new Mock<IModelInitializer>();

            mock.Setup(m => m.InitModelConfigurations(assembly)).Returns(new IEntityConfiguration[]
            {
                new OfflineExcerptNormativeTopicConfiguration(), new OwnerConfiguration()
            });

            var modelInitializer = new ModelInitializerWithSetAllNullInverseReferencePropertiesDecorator(mock.Object);

            var eConfigs = modelInitializer.InitModelConfigurations(assembly);
            var eConfig = eConfigs.OfType<OfflineExcerptNormativeTopicConfiguration>().First();
            var property = eConfig.Properties.OfType<ManyToOneWithInversPropertyRelationshipProperty>().FirstOrDefault();

            Assert.IsNotNull(property, "Many-to-one property configuration is required.");
        }
    }
}
using Moq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Initialization;
using Oentities.Tests.EntityConfigurations;
using System;
using System.Reflection;

namespace Oentities.Tests.Initialization
{
    [TestFixture]
    public class ModelInitializerWithOnlyUniqueTypesDecoratorTests
    {
        [Test]
        public void WhenModelConfigsContainsNonUniqueTypesThenThrowInvalidOperationException()
        {
            var assembly = GetType().Assembly;

            var modelInitializer = CreateModelInitializerWithConfigureMock(
                assembly,
                new OfflineExcerptNormativeTopicConfiguration(),
                new OfflineExcerptNormativeTopicConfiguration(),
                new OwnerConfiguration());

            const string error = "Expected throw InvalidOperationException because model configs contains not unique types.";

            Assert.Throws<InvalidOperationException>(() => modelInitializer.InitModelConfigurations(assembly), error);
        }

        [Test]
        public void WhenModelConfigsContainsOnlyUniqueTypesThenNotThrowInvalidOperationException()
        {
            var assembly = GetType().Assembly;

            var modelInitializer = CreateModelInitializerWithConfigureMock(
                assembly,
                new OfflineExcerptNormativeTopicConfiguration(),
                new OwnerConfiguration());

            const string error = "Expected no throw InvalidOperationException because model configs contains only unique types.";

            Assert.DoesNotThrow(() => modelInitializer.InitModelConfigurations(assembly), error);
        }

        [Test]
        public void WhenModelConfigsContainsUniqueTypesThenReturnUnchangedEntityConfigs()
        {
            var assembly = GetType().Assembly;

            var eConfigsExpected = new IEntityConfiguration[]
            {
                new OfflineExcerptNormativeTopicConfiguration(),
                new OwnerConfiguration()
            };

            var modelInitializer = CreateModelInitializerWithConfigureMock(assembly, eConfigsExpected);

            var eConfigsActual = modelInitializer.InitModelConfigurations(assembly);

            const string error = "Expected when model configs contains only unique types then return unchanged entity configs.";

            CollectionAssert.AreEqual(eConfigsExpected, eConfigsActual, error);
        }

        private static IModelInitializer CreateModelInitializerWithConfigureMock(
            Assembly assembly, 
            params IEntityConfiguration[] returnResultForMock)
        {
            var mock = new Mock<IModelInitializer>();
            mock.Setup(m => m.InitModelConfigurations(assembly)).Returns(returnResultForMock);
            return new ModelInitializerWithOnlyUniqueTypesDecorator(mock.Object);
        }
    }
}
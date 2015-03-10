using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Initialization;
using Oentities.Tests.EntityConfigurations;

namespace Oentities.Tests.Initialization
{
    [TestFixture]
    public class DefaultModelInitializerTests
    {
        [Test]
        public void ModelConfigurationsConsistsOfRightAmountAndHasNecessaryTypesTest()
        {
            const int eConfigsCount = 10;

            var eConfigs = new DefaultModelInitializer().InitModelConfigurations(GetType().Assembly);

            Assert.IsNotNull(eConfigs, "Model Configurations is required.");
            Assert.IsTrue(eConfigs.Count == eConfigsCount, "Model configurations consist not of the right amount.");
            AssertEntityConfigurationExist<OfflineExcerptNormativeTopicConfiguration>(eConfigs);
            AssertEntityConfigurationExist<OwnerConfiguration>(eConfigs);
            AssertEntityConfigurationExist<OilRigConfiguration>(eConfigs);
            AssertEntityConfigurationExist<OilfieldConfiguration>(eConfigs);
            AssertEntityConfigurationExist<UniversityConfiguration>(eConfigs);
            AssertEntityConfigurationExist<LectorConfiguration>(eConfigs);
        }

        private static void AssertEntityConfigurationExist<TEntityConfiguration>(IEnumerable<IEntityConfiguration> eConfigs)
        {
            var error = string.Format("Model configurations is not exist [{0}].", typeof(TEntityConfiguration).Name);
            Assert.IsTrue(eConfigs.Any(c => c is TEntityConfiguration), error);
        }
    }
}
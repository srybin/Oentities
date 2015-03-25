using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Tests.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oentities.Tests.Configurations
{
    [TestFixture]
    public class ModelBuilderTests
    {
        [Test]
        public void WhenModelBuiltThroughAddMethodContainsNonUniqueTypesThenThrowInvalidOperationException()
        {
            var builder = new ModelBuilder();

            const string error = "Expected throw InvalidOperationException because model configs contains not unique types.";

            Assert.Throws<InvalidOperationException>(() => builder.Add(new ConferenceConfiguration()).Add(new ConferenceConfiguration()), error);
        }

        [Test]
        public void WhenModelBuiltFromAssemblyContainsOnlyUniqueTypesThenNotThrowInvalidOperationException()
        {
            const string error = "Expected no throw InvalidOperationException because model configs contains only unique types.";

            Assert.DoesNotThrow(() => new ModelBuilder().BuildFrom(GetType().Assembly), error);
        }

        [Test]
        public void WhenModelBuiltThroughAddMethodContainsOnlyUniqueTypesThenNotThrowInvalidOperationException()
        {
            const string error = "Expected no throw InvalidOperationException because model configs contains only unique types.";

            Assert.DoesNotThrow(() => new ModelBuilder().Add(new OwnerConfiguration()).Add(new OfflineExcerptNormativeTopicConfiguration()), error);
        }

        [Test]
        public void WhenModelBuiltThroughIgnoreMethodThenConfigurationIsNoExistInModel()
        {
            var builder = new ModelBuilder();

            builder.BuildFrom(GetType().Assembly).Ignore<ConferenceConfiguration>();

            const string error = "Expected on entity configuration is no exist in model";

            Assert.IsNull(builder.Configurations.FirstOrDefault(c => c.GetType() == typeof(ConferenceConfiguration)), error);
        }

        [Test]
        public void ForAllOneToManyWithoutInversPropertyConfigurationSetInversPropertyConfiguration()
        {
            var builder = new ModelBuilder();

            builder.Add(new OwnerConfiguration()).Add(new OfflineExcerptNormativeTopicConfiguration());
            builder.SetAllNullInverseReferenceProperties();

            var eConfig = builder.Configurations.OfType<OfflineExcerptNormativeTopicConfiguration>().First();
            var property = eConfig.Properties.OfType<ManyToOneWithInversPropertyRelationshipProperty>().FirstOrDefault();

            Assert.IsNotNull(property, "Many-to-one property configuration is required.");
        }

        [Test]
        public void ForAllManyToManyWithoutInversPropertyConfigurationSetInversPropertyConfiguration()
        {
            var builder = new ModelBuilder();

            builder.Add(new CourseConfiguration()).Add(new StudentConfiguration());
            builder.SetAllNullInverseReferenceProperties();

            var eConfig = builder.Configurations.OfType<StudentConfiguration>().First();
            var property = eConfig.Properties.OfType<ManyToManyWithInversPropertyRelationshipProperty>().FirstOrDefault();

            Assert.IsNotNull(property, "Many-to-many property configuration is required.");
        }

        [Test]
        public void ModelConfigurationsConsistsOfRightAmountAndHasNecessaryTypesTest()
        {
            const int eConfigsCount = 10;

            var eConfigs = new ModelBuilder().BuildFrom(GetType().Assembly).Configurations;

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
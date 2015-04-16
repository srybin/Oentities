using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Tests.Entities;
using Oentities.Tests.EntityConfigurations;

namespace Oentities.Tests.Configurations
{
    [TestFixture]
    public class OneToManyRelationshipConfigurationsTests : RelationshipConfigurationsTestsBase
    {
        [Test]
        public void OneToManyWithoutInversePropertyConfigurationTest()
        {
            var property = new OwnerConfiguration().Properties.OfType<OneToManyWithoutInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversePropertyConfigurationType<ManyToOneWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Owner, OfflineExcerptNormativeTopic>(property, RelationshipSide.One, RelationshipSide.Many);
            AssertPropertyType<ICollection<OfflineExcerptNormativeTopic>>(property, RelationshipSide.One);
        }

        [Test]
        public void ManyToOneWithoutInversePropertyConfigurationTest()
        {
            var property = new OilRigConfiguration().Properties.OfType<ManyToOneWithoutInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversePropertyConfigurationType<OneToManyWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<OilRig, Oilfield>(property, RelationshipSide.Many, RelationshipSide.One);
            AssertPropertyType<Oilfield>(property, RelationshipSide.Many);
        }

        [Test]
        public void OneToManyWithInversePropertyConfigurationTest()
        {
            var property = new UniversityConfiguration().Properties.OfType<OneToManyWithInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversePropertyConfigurationType<ManyToOneWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<University, Lector>(property, RelationshipSide.One, RelationshipSide.Many);
            AssertPropertyType<ICollection<Lector>>(property, RelationshipSide.One);
        }

        [Test]
        public void ManyToOneWithInversePropertyConfigurationTest()
        {
            var property = new LectorConfiguration().Properties.OfType<ManyToOneWithInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversePropertyConfigurationType<OneToManyWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Lector, University>(property, RelationshipSide.Many, RelationshipSide.One);
            AssertPropertyType<University>(property, RelationshipSide.Many);
        }
    }
}
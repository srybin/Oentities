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
        public void TestOneToManyWithoutInversPropertyConfiguration()
        {
            var property = new OwnerConfiguration().Properties.OfType<OneToManyWithoutInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversPropertyConfigurationType<ManyToOneWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Owner, OfflineExcerptNormativeTopic>(property, RelationshipSide.One, RelationshipSide.Many);
            AssertPropertyType<ICollection<OfflineExcerptNormativeTopic>>(property, RelationshipSide.One);
        }


        [Test]
        public void TestManyToOneWithoutInversPropertyConfiguration()
        {
            var property = new OilRigConfiguration().Properties.OfType<ManyToOneWithoutInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversPropertyConfigurationType<OneToManyWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<OilRig, Oilfield>(property, RelationshipSide.Many, RelationshipSide.One);
            AssertPropertyType<Oilfield>(property, RelationshipSide.Many);
        }

        [Test]
        public void TestOneToManyWithInversPropertyConfiguration()
        {
            var property = new UniversityConfiguration().Properties.OfType<OneToManyWithInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversPropertyConfigurationType<ManyToOneWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<University, Lector>(property, RelationshipSide.One, RelationshipSide.Many);
            AssertPropertyType<ICollection<Lector>>(property, RelationshipSide.One);
        }

        [Test]
        public void TestManyToOneWithInversPropertyConfiguration()
        {
            var property = new LectorConfiguration().Properties.OfType<ManyToOneWithInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversPropertyConfigurationType<OneToManyWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Lector, University>(property, RelationshipSide.Many, RelationshipSide.One);
            AssertPropertyType<University>(property, RelationshipSide.Many);
        }
    }
}
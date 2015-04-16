using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Tests.Entities;
using Oentities.Tests.EntityConfigurations;

namespace Oentities.Tests.Configurations
{
    [TestFixture]
    public class ManyToManyRelationshipConfigurationsTests : RelationshipConfigurationsTestsBase
    {
        [Test]
        public void ManyToManyWithoutInversePropertyConfigurationTest()
        {
            var property = new CourseConfiguration().Properties.OfType<ManyToManyWithoutInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversePropertyConfigurationType<ManyToManyWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Course, Student>(property, RelationshipSide.Many, RelationshipSide.Many);
            AssertPropertyType<ICollection<Student>>(property, RelationshipSide.Many);
        }

        [Test]
        public void ManyToManyWithInversePropertyConfigurationTest()
        {
            var property = new ConferenceConfiguration().Properties.OfType<ManyToManyWithInversePropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversePropertyConfigurationType<ManyToManyWithInversePropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Conference, PresentationMaterial>(property, RelationshipSide.Many, RelationshipSide.Many);
            AssertPropertyType<ICollection<PresentationMaterial>>(property, RelationshipSide.Many);
        }
    }
}
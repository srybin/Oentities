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
        public void ManyToManyWithoutInversPropertyConfigurationTest()
        {
            var property = new CourseConfiguration().Properties.OfType<ManyToManyWithoutInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, false);
            AssertInversPropertyConfigurationType<ManyToManyWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Course, Student>(property, RelationshipSide.Many, RelationshipSide.Many);
            AssertPropertyType<ICollection<Student>>(property, RelationshipSide.Many);
        }

        [Test]
        public void ManyToManyWithInversPropertyConfigurationTest()
        {
            var property = new ConferenceConfiguration().Properties.OfType<ManyToManyWithInversPropertyRelationshipProperty>()
                .FirstOrDefault();

            AssertRelationshipConfigurationExist(property, true);
            AssertInversPropertyConfigurationType<ManyToManyWithInversPropertyRelationshipProperty>(property);
            AssertTypesOfBothSides<Conference, PresentationMaterial>(property, RelationshipSide.Many, RelationshipSide.Many);
            AssertPropertyType<ICollection<PresentationMaterial>>(property, RelationshipSide.Many);
        }
    }
}
using NUnit.Framework;
using Oentities.Configurations;

namespace Oentities.Tests.Configurations
{
    public abstract class RelationshipConfigurationsTestsBase
    {
        protected static void AssertRelationshipConfigurationExist(RelationshipProperty property, bool inversPropertyExist)
        {
            Assert.IsNotNull(property, "Relationship property is no exist.");
            Assert.IsNotNull(property.InversProperty, "Invers property configuration is required.");

            if (inversPropertyExist)
                Assert.IsNotNull(property.InversProperty.Info, "Invers property is required.");
            else
                Assert.IsNull(property.InversProperty.Info, "Invers property must be missing.");
        }

        protected static void AssertInversPropertyConfigurationType<TInversRelationshipProperty>(RelationshipProperty property)
        {
            Assert.IsInstanceOf<TInversRelationshipProperty>(property.InversProperty, "Invers property has an incorrect type.");
        }

        protected static void AssertTypesOfBothSides<T1, T2>(
            RelationshipProperty property, 
            string firstSideType, 
            string secondSideType)
        {
            const string errorFormat = "'{0}' side of the relationship has an incorrect type.";
            var errorForT1 = string.Format(errorFormat, firstSideType);
            var errorForT2 = string.Format(errorFormat, secondSideType);

            Assert.AreEqual(typeof(T1), property.EntityType, errorForT1);
            Assert.AreEqual(typeof(T2), property.InversProperty.EntityType, errorForT2);
        }

        protected static void AssertPropertyType<T>(RelationshipProperty property, string fromSide)
        {
            var error = string.Format("Property from '{0}' side has an incorrect type.", fromSide);
            Assert.AreEqual(typeof(T), property.Info.PropertyType, error);
        }

        protected static class RelationshipSide
        {
            public const string One = "One";
            public const string Many = "Many";
        }
    }
}
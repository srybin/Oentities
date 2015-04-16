using NUnit.Framework;
using Oentities.Configurations;

namespace Oentities.Tests.Configurations
{
    public abstract class RelationshipConfigurationsTestsBase
    {
        protected static void AssertRelationshipConfigurationExist(RelationshipProperty property, bool inversePropertyExist)
        {
            Assert.IsNotNull(property, "Relationship property is no exist.");
            Assert.IsNotNull(property.InverseProperty, "Inverse property configuration is required.");

            if (inversePropertyExist)
                Assert.IsNotNull(property.InverseProperty.Info, "Inverse property is required.");
            else
                Assert.IsNull(property.InverseProperty.Info, "Inverse property must be missing.");
        }

        protected static void AssertInversePropertyConfigurationType<TInverseRelationshipProperty>(RelationshipProperty property)
        {
            Assert.IsInstanceOf<TInverseRelationshipProperty>(property.InverseProperty, "Inverse property has an incorrect type.");
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
            Assert.AreEqual(typeof(T2), property.InverseProperty.EntityType, errorForT2);
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
using System;
using System.Reflection;
using Oentities.Extensions;

namespace Oentities.Configurations
{
    public abstract class Property
    {
        public PropertyInfo Info { get; set; }
        public Type EntityType { get; set; }
    }

    public class PrimitiveProperty : Property
    {
    }

    public abstract class RelationshipProperty : Property
    {
        public RelationshipProperty InverseProperty { get; set; }

        public static RelationshipProperty Create<TRelationshipProperty, TInverseRelationshipProperty>(
            PropertyInfo property, 
            PropertyInfo inverseProperty)
            where TRelationshipProperty : RelationshipProperty, new()
            where TInverseRelationshipProperty : RelationshipProperty, new()
        {
            var relationship = new TRelationshipProperty
            {
                Info = property,
                EntityType = property.DeclaringType
            };

            relationship.InverseProperty = new TInverseRelationshipProperty
            {
                Info = inverseProperty,
                EntityType = property.PropertyType.IsCollectionType() ? property.PropertyType.GetEntityType() : property.PropertyType,
                InverseProperty = relationship
            };

            return relationship;
        }
    }

    public class OneToManyWithoutInversePropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class OneToManyWithInversePropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToOneWithoutInversePropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToOneWithInversePropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToManyWithoutInversePropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToManyWithInversePropertyRelationshipProperty : RelationshipProperty
    {
    }
}
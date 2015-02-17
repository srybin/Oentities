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

    public class RelationshipProperty : Property
    {
        public RelationshipProperty InversProperty { get; set; }

        public static RelationshipProperty Create<TRelationshipProperty, TInversRelationshipProperty>(
            PropertyInfo property, 
            PropertyInfo inversProperty)
            where TRelationshipProperty : RelationshipProperty, new()
            where TInversRelationshipProperty : RelationshipProperty, new()
        {
            var relationship = new TRelationshipProperty
            {
                Info = property,
                EntityType = property.DeclaringType
            };

            relationship.InversProperty = new TInversRelationshipProperty
            {
                Info = inversProperty,
                EntityType = property.PropertyType.IsCollectionType() ? property.PropertyType.GetEntityType() : property.PropertyType,
                InversProperty = relationship
            };

            return relationship;
        }
    }

    public class OneToManyWithoutInversPropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class OneToManyWithInversPropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToOneWithoutInversPropertyRelationshipProperty : RelationshipProperty
    {
    }

    public class ManyToOneWithInversPropertyRelationshipProperty : RelationshipProperty
    {
    }
}
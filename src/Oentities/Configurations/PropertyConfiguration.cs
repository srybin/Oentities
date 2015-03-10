using System;
using System.Linq.Expressions;
using System.Reflection;
using Oentities.Extensions;

namespace Oentities.Configurations
{
    public class PropertyConfiguration<TEntity>
    {
        private readonly PropertyInfo _property;
        private readonly IEntityConfiguration _configuration;

        public PropertyConfiguration(PropertyInfo property, IEntityConfiguration configuration)
        {
            _property = property;
            _configuration = configuration;
        }

        public void WithMany()
        {
            if (_property.PropertyType.IsPrimitiveType())
                throw new Exception("Property type is...");

            var property = _property.PropertyType.IsCollectionType()
                ? RelationshipProperty.Create<ManyToManyWithoutInversPropertyRelationshipProperty, ManyToManyWithInversPropertyRelationshipProperty>(_property, null)
                : RelationshipProperty.Create<ManyToOneWithoutInversPropertyRelationshipProperty, OneToManyWithInversPropertyRelationshipProperty>(_property, null);

            _configuration.Properties.Add(property);
        }

        public void WithMany<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            if (_property.PropertyType.IsPrimitiveType())
                throw new Exception("Property type is...");

            var relationshipProperty = _property.PropertyType.IsCollectionType()
                ? RelationshipProperty.Create<ManyToManyWithInversPropertyRelationshipProperty, ManyToManyWithInversPropertyRelationshipProperty>(_property, property.GetPropertyInfoBy())
                : RelationshipProperty.Create<ManyToOneWithInversPropertyRelationshipProperty, OneToManyWithInversPropertyRelationshipProperty>(_property, property.GetPropertyInfoBy());

            _configuration.Properties.Add(relationshipProperty);
        }

        public void WithOne()
        {
            if (_property.PropertyType.IsPrimitiveType() || !_property.PropertyType.IsCollectionType())
                throw new Exception("Property type no collection");

            _configuration.Properties.Add(RelationshipProperty.Create<OneToManyWithoutInversPropertyRelationshipProperty, ManyToOneWithInversPropertyRelationshipProperty>(_property, null));
        }

        public void WithOne<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            if (_property.PropertyType.IsPrimitiveType() || !_property.PropertyType.IsCollectionType())
                throw new Exception("Property type is no collection");

            _configuration.Properties.Add(RelationshipProperty.Create<OneToManyWithInversPropertyRelationshipProperty, ManyToOneWithInversPropertyRelationshipProperty>(_property, property.GetPropertyInfoBy()));
        }
    }
}
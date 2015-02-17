using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Oentities.Extensions;

namespace Oentities.Configurations
{
    public class EntityConfiguration<TEntity> : IEntityConfiguration
    {
        public EntityConfiguration()
        {
            EntityType = typeof(TEntity);
            TableName = string.Concat(EntityType.Name, "s");
            Key = EntityType.GetProperties().Where(p => p.GetSetMethod() != null).First(p => p.Name == "Id");

            Properties = EntityType.GetProperties()
                .Where(p => p.GetSetMethod() != null && p.Name != "Id" && p.PropertyType.IsPrimitiveType())
                .Select(p => new PrimitiveProperty{Info = p, EntityType = EntityType})
                .Cast<Property>()
                .ToList();
        }

        public Type EntityType { get; private set; }
        public string TableName { get; private set; }
        public PropertyInfo Key { get; private set; }
        public ICollection<Property> Properties { get; private set; }

        protected PropertyConfiguration<TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            return new PropertyConfiguration<TProperty>(property.GetPropertyInfoBy(), this);
        }

        protected PropertyConfiguration<TProperty> Property<TProperty>(Expression<Func<TEntity, ICollection<TProperty>>> property)
        {
            return new PropertyConfiguration<TProperty>(property.GetPropertyInfoBy(), this);
        }
    }
}
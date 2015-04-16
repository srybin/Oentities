namespace Oentities.Configurations
{
    interface IPropertyFromEntityAccessor
    {
        object Get(object entity, Property property);
    }
}
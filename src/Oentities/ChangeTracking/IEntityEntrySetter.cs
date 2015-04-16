using Oentities.Configurations;

namespace Oentities.ChangeTracking
{
    interface IEntityEntrySetter
    {
        void ForEachEntitiesFromCollectionEntitySetHer();

        void ForEachEntitiesFromCollectionEntitySetThis(object entity, RelationshipProperty property);

        void FindAndSetInverseSideFor(object entity, RelationshipProperty property);
    }
}
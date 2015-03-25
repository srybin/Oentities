using NUnit.Framework;
using Oentities.ChangeTracking;
using Oentities.Tests.Entities;

namespace Oentities.Tests.ChangeTracking
{
    [TestFixture]
    public class ChangeTrackerTests
    {
        [Test]
        public void WhenEntityMarkNewThenThisEntityExistInEntriesAndHaveTrueState()
        {
            AssertAttach(new OfflineExcerptNormativeTopic(), EntityState.New);
        }

        [Test]
        public void WhenEntityMarkDirtyThenThisEntityExistInEntriesAndHaveTrueState()
        {
            AssertAttach(new Conference(), EntityState.Dirty);
        }

        [Test]
        public void WhenEntityMarkDeleteThenThisEntityExistInEntriesAndHaveTrueState()
        {
            AssertAttach(new Owner(), EntityState.Deleted);
        }

        [Test]
        public void WhenEntityMarkCleanThenThisEntityExistInEntriesAndHaveTrueState()
        {
            AssertAttach(new University(), EntityState.Clean);
        }

        [Test]
        public void WhenAnyEntitiesIsNotCleanThenHasChangesEqualsTrue()
        {
            var changeTracker = new ChangeTracker();

            AttachMultipleEntitiesWith(EntityState.Dirty, changeTracker);

            Assert.IsTrue(changeTracker.HasChanges(), "Change tracker does not see the entities changes.");
        }

        [Test]
        public void WhenAllEntitiesIsCleanThenHasChangesEqualsFalse()
        {
            var changeTracker = new ChangeTracker();

            AttachMultipleEntitiesWith(EntityState.Clean, changeTracker);

            Assert.IsFalse(changeTracker.HasChanges(), "Change tracker mistakenly sees entities changes.");
        }

        [Test]
        public void WhenEntriesIsEmptyThenHasChangesEqualsFalse()
        {
            Assert.IsFalse(new ChangeTracker().HasChanges(), "Change tracker sees change without tracking entities.");
        }

        private static void AttachMultipleEntitiesWith(EntityState state, ChangeTracker changeTracker)
        {
            changeTracker.Attach(new OfflineExcerptNormativeTopic(), state);
            changeTracker.Attach(new Owner(), state);
            changeTracker.Attach(new Conference(), state);
        }

        private static void AssertAttach(object entity, EntityState state)
        {
            var changeTracker = new ChangeTracker();

            changeTracker.Attach(entity, state);
            var entry = changeTracker.Entry(entity);

            Assert.IsNotNull(entity, "Entity entry is no exist.");
            Assert.AreEqual(entity, entry.Entity, "Entity entry contains an incorrect entity reference.");
            Assert.AreEqual(state, entry.State, "Entity entry contains an incorrect state.");
        }
    }
}

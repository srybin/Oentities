using System;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Oentities.Configurations;
using Oentities.Tests.Entities;
using Oentities.Tests.EntityConfigurations;

namespace Oentities.Tests.ChangeTracking
{
    [TestFixture]
    public class EntityEntrySetterTests
    {
        [Test]
        public void WhenEntityOneSideMarkCleanSetThisForEachEntitiesFromCollectionEntity()
        {
            var e = new Owner
            {
                Id = 4,
                Topics = new Collection<OfflineExcerptNormativeTopic>
                {
                    new OfflineExcerptNormativeTopic { Id = 4, LastModified = DateTime.UtcNow, Description = "offline 3.0" }
                }
            };

            var t = e.Topics.First();

            var uow = new UnitOfWorkForTest();
            uow.MarkClean(t);
            uow.MarkClean(e);

            AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(e, t, uow);
        }

        [Test]
        public void WhenEntityManySideMarkCleanFindAndSetInverseSide()
        {
            var e = new Owner
            {
                Id = 4,
                Topics = new Collection<OfflineExcerptNormativeTopic>
                {
                    new OfflineExcerptNormativeTopic { Id = 4, LastModified = DateTime.UtcNow, Description = "offline 3.0" }
                }
            };

            var t = e.Topics.First();

            var uow = new UnitOfWorkForTest();
            uow.MarkClean(e);
            uow.MarkClean(t);

            AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(e, t, uow);
        }

        [Test]
        public void WhenExecutingCommitForEachEntitiesFromCollectionEntitySetHer()
        {
            var o1 = new Owner {Id = 4};
            var o2 = new Owner {Id = 3};
            var t1 = new OfflineExcerptNormativeTopic {Id = 4};
            var t2 = new OfflineExcerptNormativeTopic {Id = 3};
            var t3 = new OfflineExcerptNormativeTopic {Id = 2};

            var uow = new UnitOfWorkForTest();

            uow.MarkClean(o1);
            uow.MarkClean(o2);
            uow.MarkClean(t1);
            uow.MarkClean(t2);
            uow.MarkClean(t3);

            o1.Topics = new Collection<OfflineExcerptNormativeTopic> {t1};
            o2.Topics = new Collection<OfflineExcerptNormativeTopic> {t2, t3};

            uow.Commit();

            AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(o1, t1, uow);
            AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(o2, t2, uow);
            AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(o2, t3, uow);
        }

        private static void AssertThatEntityWithoutInversePropertyContainsExternalLinkToAn(
            object expectedExternalLink, 
            object entity, 
            UnitOfWork unitOfWork)
        {
            const string error = "Expected that the entity without inverse property contains external link.";
            Assert.AreEqual(expectedExternalLink, unitOfWork.Entries[entity].ExternalLinks["Owner_Id"], error);
        }
    }

    class UnitOfWorkForTest : UnitOfWork
    {
        protected override void ModelInit(IModelBuilder modelBuilder)
        {
            modelBuilder.Add(new OwnerConfiguration()).Add(new OfflineExcerptNormativeTopicConfiguration());
        }
    }
}
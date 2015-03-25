using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class ConferenceConfiguration : EntityConfiguration<Conference>
    {
        public ConferenceConfiguration()
        {
            Property(e => e.PresentationMaterialsForMulticasting).WithMany(e => e.PastPublicConferences);
        }
    }
}
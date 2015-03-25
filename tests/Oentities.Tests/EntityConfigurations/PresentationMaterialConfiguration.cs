using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class PresentationMaterialConfiguration : EntityConfiguration<PresentationMaterial>
    {
        public PresentationMaterialConfiguration()
        {
            Property(e => e.PastPublicConferences).WithMany(e => e.PresentationMaterialsForMulticasting);
        }
    }
}
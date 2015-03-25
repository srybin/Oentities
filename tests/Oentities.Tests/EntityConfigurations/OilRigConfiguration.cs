using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class OilRigConfiguration : EntityConfiguration<OilRig>
    {
        public OilRigConfiguration()
        {
            Property(e => e.Oilfield).WithMany();
        }
    }
}
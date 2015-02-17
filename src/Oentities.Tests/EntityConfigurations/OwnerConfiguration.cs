using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class OwnerConfiguration : EntityConfiguration<Owner>
    {
        public OwnerConfiguration()
        {
            Property(e => e.Topics).WithOne();
        }
    }
}
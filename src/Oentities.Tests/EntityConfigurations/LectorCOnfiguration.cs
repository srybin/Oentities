using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class LectorConfiguration : EntityConfiguration<Lector>
    {
        public LectorConfiguration()
        {
            Property(e => e.University).WithMany(e => e.Lectors);
        } 
    }
}
using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class UniversityConfiguration : EntityConfiguration<University>
    {
        public UniversityConfiguration()
        {
            Property(e => e.Lectors).WithOne(e => e.University);
        }
    }
}
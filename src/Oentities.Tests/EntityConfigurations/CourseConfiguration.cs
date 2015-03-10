using Oentities.Configurations;
using Oentities.Tests.Entities;

namespace Oentities.Tests.EntityConfigurations
{
    class CourseConfiguration : EntityConfiguration<Course>
    {
        public CourseConfiguration()
        {
            Property(e => e.Students).WithMany();
        }
    }
}
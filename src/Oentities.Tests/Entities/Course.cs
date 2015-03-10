using System;
using System.Collections.Generic;

namespace Oentities.Tests.Entities
{
    public class Course : Entity<Guid>
    {
        public string Content { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
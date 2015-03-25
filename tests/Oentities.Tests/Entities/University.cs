using System.Collections.Generic;

namespace Oentities.Tests.Entities
{
    public class University : Entity<int>
    {
        public string Title { get; set; }
        public ICollection<Lector> Lectors { get; set; }
    }
}
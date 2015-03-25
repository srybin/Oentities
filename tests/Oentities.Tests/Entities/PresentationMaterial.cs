using System.Collections.Generic;

namespace Oentities.Tests.Entities
{
    public class PresentationMaterial : Entity<int>
    {
        public string Content { get; set; }
        public ICollection<Conference> PastPublicConferences { get; set; }
    }
}
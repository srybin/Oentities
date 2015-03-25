using System.Collections.Generic;

namespace Oentities.Tests.Entities
{
    public class Owner : Entity<int>
    {
        public string FullDescription { get; set; }
        public ICollection<OfflineExcerptNormativeTopic> Topics { get; set; }
    }
}
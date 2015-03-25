using System.Collections.Generic;

namespace Oentities.Tests.Entities
{
    public class Conference : Entity<int>
    {
        public string Title { get; set; }
        public ICollection<PresentationMaterial> PresentationMaterialsForMulticasting { get; set; }
    }
}
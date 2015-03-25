using System;

namespace Oentities.Tests.Entities
{
    public class OilRig : Entity<Guid>
    {
        public string Number { get; set; }
        public Oilfield Oilfield { get; set; }
    }
}
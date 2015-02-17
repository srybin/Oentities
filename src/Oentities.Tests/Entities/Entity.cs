using System;

namespace Oentities.Tests.Entities
{
    public abstract class Entity<TIdentity>
    {
        public TIdentity Id { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public bool IsNew
        {
            get { return Equals(Id, default(TIdentity)); }
        }
    }
}
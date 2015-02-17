namespace Oentities.Tests.Entities
{
    public class Lector : Entity<int>
    {
        public string Name { get; set; }
        public University University { get; set; }
    }
}
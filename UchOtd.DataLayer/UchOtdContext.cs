using System.Data.Entity;
using UchOtd.DomainClasses;

namespace UchOtd
{
    public class UchOtdContext : DbContext
    {
        public UchOtdContext()
            : base("Name=UchOtdConnection")
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}

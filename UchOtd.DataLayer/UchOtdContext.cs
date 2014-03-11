using System.Data.Entity;
using UchOtd.DomainClasses;

namespace UchOtd
{
    public class UchOtdContext : DbContext
    {
        public UchOtdContext()
            : base()
        {
        }
        public UchOtdContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}

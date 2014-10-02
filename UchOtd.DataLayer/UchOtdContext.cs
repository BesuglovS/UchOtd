using System.Data.Entity;
using UchOtd.DomainClasses;

namespace UchOtd
{
    public class UchOtdContext : DbContext
    {
        public UchOtdContext()
            : base("data source=tcp:127.0.0.1,1433;Database=UchOtd;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True")
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

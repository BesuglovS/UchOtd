using System.Data.Entity;
using UchOtd.DomainClasses;

namespace UchOtd.DataLayer
{
    public class UchOtdContext : DbContext
    {
        public UchOtdContext()
            : base("data source=tcp:" + "127.0.0.1" + ",1433;Database=UchOtd; Integrated Security = SSPI; multipleactiveresultsets=True")
        {
            Database.CommandTimeout = 180;
        }

        public UchOtdContext(string connectionString)
            : base(connectionString)
        {
            Database.CommandTimeout = 180;
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}

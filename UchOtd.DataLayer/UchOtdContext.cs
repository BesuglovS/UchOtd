using System.Data.Entity;
using UchOtd.DomainClasses;

namespace UchOtd.DataLayer
{
    public class UchOtdContext : DbContext
    {
        public UchOtdContext()
            : base("data source=tcp:" + @"UCH-OTD-DISP-2\SQLEXPRESS" + ",1433;Database=UchOtd; User Id=sa; Password=ghjuhfvvf; multipleactiveresultsets=True")
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

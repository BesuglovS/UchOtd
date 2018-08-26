using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using UchOtd.DomainClasses;

namespace UchOtd.DataLayer
{
    public class UchOtdContext : DbContext
    {
        private readonly string _connectionString;

        public UchOtdContext()
            : base(GetConnectionString())
        {
            _connectionString = GetConnectionString();
            Database.CommandTimeout = 180;
        }

        private static string GetConnectionString()
        {
            return "data source=tcp:" + @".\SQLEXPRESS" +
                   ",1433;Database=UchOtd; User Id=sa; Password=ghjuhfvvf; multipleactiveresultsets=True";
        }

        public UchOtdContext(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
            Database.CommandTimeout = 180;
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Phone> Phones { get; set; }
    }
}

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using iPgBilal.Domain.Entities;

namespace IPgBilal.DAL
{
    public class IpgBilalbContext : DbContext
    {
        public IpgBilalbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new BookLoanMapping());
        }
    }
}
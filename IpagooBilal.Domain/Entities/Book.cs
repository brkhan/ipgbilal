using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;

namespace iPgBilal.Domain.Entities
{
    public class Book
    {
        public Book()
        {
            BookLoans = new List<BookLoan>();
        }
        public int BookId { get; set; }
        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public virtual ICollection<BookLoan> BookLoans { get; set; }

        public bool IsAvailableToLoan
        {
           
            get { return !BookLoans.Any() || BookLoans.OrderByDescending(a => a.LoanStartDate).First().LoanEndDate != null; }
        }

    }

    public class BookMapping : EntityTypeConfiguration<Book>
    {
        public BookMapping()
        {
            Map(o => o.ToTable("Book", "dbo"));
            HasKey(o => o.BookId);
          }
    }

    public class BookLoanMapping : EntityTypeConfiguration<BookLoan>
    {
        public BookLoanMapping()
        {
            Map(o => o.ToTable("BookLoan", "dbo"));
            HasKey(o => o.BookLoanId);
     
        }
    }
}
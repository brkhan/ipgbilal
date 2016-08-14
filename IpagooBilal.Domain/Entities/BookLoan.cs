using System;

namespace iPgBilal.Domain.Entities
{
    public class BookLoan
    {
        public int BookLoanId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanStartDate { get; set; }
        public DateTime? LoanEndDate { get; set; }

        public string LoanedTo { get; set; }
        public string LoanedToAddress { get; set; }
        public string Comment { get; set; }
        public Book Book { get; set; }
    }
}
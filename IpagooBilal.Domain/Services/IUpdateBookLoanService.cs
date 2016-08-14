using iPgBilal.Domain.Entities;

namespace iPgBilal.Domain.Services
{



    public interface IUpdateBookLoanService
    {
        Book ReturnBook(string ISBN);
        Book LoanBook(string ISBN, string LoanedTo, string comments);
    }
}
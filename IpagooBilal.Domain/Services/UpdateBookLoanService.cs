using System;
using System.Linq;
using iPgBilal.Domain.Entities;
using IPgBilal.Domain;

namespace iPgBilal.Domain.Services
{
    public class UpdateBookLoanService : IUpdateBookLoanService
    {
        private readonly IRepository<Book> _repository;

        public  UpdateBookLoanService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book ReturnBook(string ISBN)
        {
            var book = _repository.Get(a => a.ISBN == ISBN,"BookLoans").Single();
            if (book.IsAvailableToLoan) return book; // back button submit
            var loan = book.BookLoans.OrderByDescending(a => a.LoanStartDate).First();
            loan.LoanEndDate = DateTime.UtcNow;
            _repository.Update(book);
            return book;
        }

        public Book LoanBook(string ISBN, string LoanedTo, string comments)
        {
            var book = _repository.Get(a => a.ISBN == ISBN,"BookLoans").Single();
            if (!book.IsAvailableToLoan) return book;
            var newLoan = new BookLoan
            {
                Comment = comments,
                LoanedTo = LoanedTo,
                LoanStartDate = DateTime.UtcNow
            };
            book.BookLoans.Add(newLoan);
            _repository.Update(book);
            return book;

        }
    }
}
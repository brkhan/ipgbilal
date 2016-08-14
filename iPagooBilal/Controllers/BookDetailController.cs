using System.Linq;
using System.Net.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using iPgBilal.Domain.Entities;
using iPgBilal.Domain.Services;
using iPgBilal.Models;
using IPgBilal.DAL;
using IPgBilal.Domain;
using Microsoft.Ajax.Utilities;

namespace iPgBilal.Controllers
{
    public class BookDetailController : Controller
    {
        private readonly ISearchBookService _searchBookService;
        private readonly IUpdateBookLoanService _updateBookLoanService;

        public BookDetailController()
        {
            //TODO DI for service
            var repository = new GenericRepository<Book>(new IpgBilalbContext());
            _searchBookService = new SearchBookService(repository);
            _updateBookLoanService = new UpdateBookLoanService(repository);
        }

        public ActionResult Detail(string isbn)
        {
            var book = _searchBookService.GetBooksBySearchCategory("ISBN", isbn,"BookLoans").Single();
            var model = new BookDetailModel(book);
            return View("Index", model);
        }

        public ActionResult Submit(BookDetailModel model)
        {
            var action = ControllerContext.HttpContext.Request["action"];
            Book updatedBook = null;
            //TODO model validation name required when loaning out

            if (action == "loanBook")
            {
                if (model.LoanTo.IsNullOrWhiteSpace())  model.LoanTo = "testname";
                 updatedBook = _updateBookLoanService.LoanBook(model.Isbn, model.LoanTo, model.Comments);
            }

            else
            {
                updatedBook = _updateBookLoanService.ReturnBook(model.Isbn);
            }

            return View("Index", new BookDetailModel(updatedBook,action));

    

        }
    }
}
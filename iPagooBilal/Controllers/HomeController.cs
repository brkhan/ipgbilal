using System.Data.Entity;
using iPgBilal.Models;
using System.Linq;
using System.Web.Mvc;
using iPgBilal.Domain.Entities;
using iPgBilal.Domain.Services;
using IPgBilal.DAL;

namespace iPgBilal.Controllers
{
    public class HomeController : Controller
    {
        public ISearchBookService SearchBookService;
        public HomeController()
        {
            //TODO
            //Inject through DI using Unity for example
            SearchBookService = new SearchBookService(new GenericRepository<Book>(new IpgBilalbContext()));
        }
 
        public ActionResult Index()
        {
            var model = new BookModel();
            return View(model);
        }

       
        [HttpPost]
        public ActionResult Submit(BookModel model)
        {
            var searchResults = SearchBookService.GetBooksBySearchCategory(model.SelectedOption,model.SearchedText);
            model.Books = searchResults.ToList();
            return View("Index",model);
        }
    }
}
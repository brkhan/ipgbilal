using iPgBilal.Domain.Entities;

namespace iPgBilal.Models
{
    public class BookDetailModel
    {
        public string RecentAction;

        public BookDetailModel()
        {

        }
        public Book Book { get; set; }
        public string LoanTo { get; set; }
        public string Comments { get; set; }
        public string Isbn { get; set; }

        public BookDetailModel(Book book, string recentAction=null)
        {
            Book = book;
            Isbn = book.ISBN;
            if (recentAction != null)
            {
                RecentAction = recentAction.Contains("return") ? "Book returned" : "Book loan action complete";
            }
           
        }

    }
}
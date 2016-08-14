using System.Collections.Generic;
using System.Web.Mvc;
using iPgBilal.Domain.Entities;

namespace iPgBilal.Models
{
    public class BookModel
    {
        public List<SelectListItem> SearchOptions { get; set; }

        public List<Book> Books { get; set; }
        public string SelectedOption { get; set; }
        public string SearchedText { get; set; }
        public BookModel()
        {
            SearchOptions = new List<SelectListItem>
            {
                new SelectListItem {Text = "Author", Value = "Author"},
                new SelectListItem {Text = "Title", Value = "Title"},
                new SelectListItem {Text = "Genre", Value = "Genre"},
                new SelectListItem {Text = "ISBN", Value = "ISBN"}
            };




            Books = new List<Book>();
        }
    }
}
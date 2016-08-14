using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using iPgBilal.Domain.Entities;
using IPgBilal.Domain;

namespace iPgBilal.Domain.Services
{
    public class SearchBookService : ISearchBookService
    {
        private readonly IRepository<Book> _bookRepo;

        public SearchBookService(IRepository<Book> bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IEnumerable<Book> GetBooksBySearchCategory(string selectedOption, string searchedText, string includeProperty = null)
        {
            return _bookRepo.Get(CreateFilter(selectedOption, searchedText), includeProperty);

       }

        private Expression<Func<Book,bool>>  CreateFilter(string selectedOption, string searchedText)
        {
            switch (selectedOption)
            {
                case "ISBN": return a => a.ISBN == searchedText;
                case "Title": return a => a.Title == searchedText;
                case "Author": return a => a.Author == searchedText;
                case "Genre": return a => a.Genre == searchedText;
            }

            throw new Exception("Invalid option selected");
        }

     }
}
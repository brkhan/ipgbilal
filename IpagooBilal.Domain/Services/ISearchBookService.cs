using System.Collections.Generic;
using iPgBilal.Domain.Entities;

namespace iPgBilal.Domain.Services
{
    public interface ISearchBookService
    {
        IEnumerable<Book> GetBooksBySearchCategory(string selectedOption, string searchedText, string loadProperty = null);
    }
}
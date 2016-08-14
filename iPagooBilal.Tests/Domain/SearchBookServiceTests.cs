using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using iPgBilal.Domain.Entities;
using iPgBilal.Domain.Services;
using IPgBilal.DAL;
using IPgBilal.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace iPgBilal.Tests.Domain
{
    [TestClass]
    public class SearchBookServiceTests
    {
        private Mock<IRepository<Book>> _mockRepository;
        private SearchBookService _service ;
        const string searchedText = "12345";

        //[SetUp]
        //public void Setup()
        //{
        //    _mockRepository = CreateMockRepository();
        //    _service = new SearchBookService(_mockRepository.Object);
        //}

        private Mock<IRepository<Book>> CreateMockRepository()
        {
            _mockRepository = new Mock<IRepository<Book>>();
            return _mockRepository;
        }

        [TestMethod]
        public void Should_Create_The_Correct_Filter_Based_On_The_Search_Category_Type()
        {
            var mockRepository = new MockBookRepository();
            _service = new SearchBookService(mockRepository);

         
            _service.GetBooksBySearchCategory("ISBN", searchedText);
        
            Assert.IsTrue(mockRepository.filterExpression.Contains("ISBN ==" ));
            Assert.IsFalse(mockRepository.filterExpression.Contains("Author ==" ));
            Assert.IsFalse(mockRepository.filterExpression.Contains("Title ==" ));
            Assert.IsFalse(mockRepository.filterExpression.Contains("Genre ==" ));
          
        }

        [TestMethod]
        public void Should_Not_Update_When_Searching_For_Book()
        {
            _service = new SearchBookService(CreateMockRepository().Object);
            _service.GetBooksBySearchCategory("ISBN", searchedText);

            _mockRepository.Verify(a=> a.Get(It.IsAny<Expression<Func<Book,bool>>>(),null),Times.Once());
            _mockRepository.Verify(a=> a.Update(It.IsAny<Book>()),Times.Never());

        }

        [TestMethod]
        public void Should_Call_The_Reposiotry_With_The_Correct_Params()
        {
            _service = new SearchBookService(CreateMockRepository().Object);
            _service.GetBooksBySearchCategory("ISBN", searchedText,"BookLoans");
            _mockRepository.Verify(a => a.Get(It.IsAny<Expression<Func<Book, bool>>>(), "BookLoans"),Times.Once());


        }

    }

    public class MockBookRepository : IRepository<Book>
    {
        public string filterExpression { get; set; }
        public IEnumerable<Book> Get(Expression<Func<Book, bool>> filter = null, string prop = null)
        {
            filterExpression = filter.ToString();
            return new List<Book>();
        }

        public void Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}

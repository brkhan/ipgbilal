using System;
using System.Collections.Generic;
using System.Linq;
using iPgBilal.Domain.Entities;
using iPgBilal.Domain.Services;
using IPgBilal.Domain;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace iPgBilal.Tests.Domain
{
    [TestFixture]
    public class UpdateBookLoanServiceTests
    {
        private Mock<IRepository<Book>> _mockRepository;
        private UpdateBookLoanService _service ;
      
        private Mock<IRepository<Book>> CreateMockRepository()
        {
            _mockRepository = new Mock<IRepository<Book>>();
            return _mockRepository;
        }

        [Test]
        public void Should_Return_Book_After_Loan()
        {
            _service = new UpdateBookLoanService(CreateMockRepository().Object);
            //Given book is currently loaned 
            var bookLoans = new List<BookLoan> { new BookLoan { LoanStartDate = DateTime.UtcNow.AddDays(-2), LoanEndDate = DateTime.UtcNow.AddDays(-1) }, new BookLoan { LoanStartDate = DateTime.UtcNow } };
            var testBook = new Book()
            {
                BookLoans =  bookLoans
            };

            Assert.AreEqual(testBook.IsAvailableToLoan, false);

            _mockRepository.Setup(a => a.Get(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<string>()))
               .Returns(new List<Book>
                {
                    testBook
                });

            //When
            _service.ReturnBook("ISBN");
           
           //Then book should be available to loan
            _mockRepository.Verify(a=> a.Get(It.IsAny<Expression<Func<Book,bool>>>(),null),Times.Never());
            _mockRepository.Verify(a=> a.Get(It.IsAny<Expression<Func<Book,bool>>>(),"BookLoans"),Times.Once());
            _mockRepository.Verify(x=> x.Update(It.Is<Book>(y=> y.IsAvailableToLoan)),Times.Once());
            _mockRepository.Verify(x => x.Update(It.Is<Book>(y => y.IsAvailableToLoan)), Times.Once());

        }

        [Test]
        public void Should_Loan_Out_A_Book()
        {
            _service = new UpdateBookLoanService(CreateMockRepository().Object);
            //Given book is currently NOT loaned 
            var bookLoans = new List<BookLoan> { new BookLoan { LoanStartDate = DateTime.UtcNow.AddDays(-2), LoanEndDate = DateTime.UtcNow.AddDays(-1) }};
            _mockRepository.Setup(a => a.Get(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<string>()))
               .Returns(new List<Book>
                {
                    new Book()
                    {
                        BookLoans =  bookLoans
                    }
                });

            //When
            _service.LoanBook("ISBN","Jack","comments");

            //Then book should be not be available to loan again
           _mockRepository.Verify(x => x.Update(It.Is<Book>(y => y.IsAvailableToLoan == false)), Times.Once());


        }

    }

}

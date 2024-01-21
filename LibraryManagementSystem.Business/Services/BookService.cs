using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public class BookService : GenericService<Book>, IBookService
    {
        private readonly IBookDal _bookDal;

        public BookService(IBookDal bookDal) : base(bookDal)
        {
            _bookDal = bookDal;
        }

        public IEnumerable<Book> GetBooksWithCategory()
        {
            return _bookDal.GetBooksWithCategory();
        }

        public IEnumerable<Book> SearchBooksWithNameOrAuthor(string query)
        {
            return _bookDal.SearchBooksWithNameOrAuthor(query);
        }

        public void DecreaseCopyCountWhenBorrowABook(Book book)
        {
            _bookDal.DecreaseCopyCountWhenBorrowABook(book);
        }
    }
}

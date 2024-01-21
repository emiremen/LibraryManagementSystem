using LibraryManagementSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public  interface IBookService : IGenericService<Book>
    {
        public IEnumerable<Book> GetBooksWithCategory();
        public IEnumerable<Book> SearchBooksWithNameOrAuthor(string query);
        public void DecreaseCopyCountWhenBorrowABook(Book book);
    }
}

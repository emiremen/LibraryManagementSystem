using LibraryManagementSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.SQLite
{
    public interface IBookDal : IGenericDal<Book>
    {
        public IEnumerable<Book> GetBooksWithCategory();
        public IEnumerable<Book> SearchBooksWithNameOrAuthor(string query);
        public void DecreaseCopyCountWhenBorrowABook(Book book);
    }
}

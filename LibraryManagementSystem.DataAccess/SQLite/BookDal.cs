using LibraryManagementSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.SQLite
{
    public class BookDal : GenericDal<Book>, IBookDal
    {
        public BookDal(SQLiteDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Book> GetBooksWithCategory()
        {
           return _dbContext.Books.Include(x => x.Category).AsNoTracking();
        }

        public IEnumerable<Book> SearchBooksWithNameOrAuthor(string query)
        {
            return _dbContext.Books.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(query) || x.Author.ToLower().Contains(query)).AsNoTracking();
        }
        public void DecreaseCopyCountWhenBorrowABook(Book book)
        {
            book.CopyCount--;
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }
    }
}

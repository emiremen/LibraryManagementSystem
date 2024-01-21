using LibraryManagementSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.SQLite
{
    public class BorrowDal: GenericDal<Borrow>, IBorrowDal
    {
        public BorrowDal(SQLiteDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Borrow> GetAllBorrowedBooks()
        {
            return _dbContext.Borrows.Include(x => x.User).Include(x => x.Book).Where(x=>!x.IsReturned);
        }

        public void GiveBackABook(Borrow borrow)
        {
            borrow.Book.CopyCount++;
            borrow.IsReturned = true;
            _dbContext.Borrows.Update(borrow);
            _dbContext.Borrows.Entry(borrow).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Borrow> SearchBookWithNameOrAuthorOrBorrowCode(string query)
        {
            return _dbContext.Borrows.Include(x => x.User).Include(x => x.Book).Where(x => x.Book.Name.ToLower().Contains(query) || x.Book.Author.ToLower().Contains(query) || x.Name.ToLower().Contains(query));
        }
        public IEnumerable<Borrow> ListExpiredBooks()
        {
            return _dbContext.Borrows.Include(x => x.User).Include(x => x.Book).Where(x => (x.ReturnDate <= DateOnly.FromDateTime(DateTime.Now)) && !x.IsReturned);
        }
    }
}

using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public class BorrowService : GenericService<Borrow>, IBorrowService
    {
        IBorrowDal _borrowDal;

        public BorrowService(IBorrowDal borrowDal) : base(borrowDal)
        {
            _borrowDal = borrowDal;
        }

        public IEnumerable<Borrow> GetAllBorrowedBooks()
        {
            return _borrowDal.GetAllBorrowedBooks();
        }

        public void GiveBackABook(Borrow borrow)
        {
            _borrowDal.GiveBackABook(borrow);
        }

        public IEnumerable<Borrow> ListExpiredBooks()
        {
            return _borrowDal.ListExpiredBooks();
        }

        public IEnumerable<Borrow> SearchBookWithNameOrAuthorOrBorrowCode(string query)
        {
            return _borrowDal.SearchBookWithNameOrAuthorOrBorrowCode(query);
        }
    }
}

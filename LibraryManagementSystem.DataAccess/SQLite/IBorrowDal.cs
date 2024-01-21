using LibraryManagementSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.SQLite
{
    public interface IBorrowDal : IGenericDal<Borrow>
    {
        public IEnumerable<Borrow> GetAllBorrowedBooks();
        public void GiveBackABook(Borrow borrow);
        public IEnumerable<Borrow> SearchBookWithNameOrAuthorOrBorrowCode(string query);
        public IEnumerable<Borrow> ListExpiredBooks();
    }
}

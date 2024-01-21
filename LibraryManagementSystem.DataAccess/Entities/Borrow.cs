using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Borrow : BaseEntity
    {
        public DateOnly ReturnDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddDays(7));
        public bool IsReturned { get; set; } = false;

        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}

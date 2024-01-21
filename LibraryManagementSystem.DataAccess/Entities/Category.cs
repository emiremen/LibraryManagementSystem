using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}

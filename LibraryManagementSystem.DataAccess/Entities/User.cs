using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Surname { get; set; }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public string Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

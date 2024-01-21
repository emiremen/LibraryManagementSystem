using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public  class Book : BaseEntity
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int CopyCount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

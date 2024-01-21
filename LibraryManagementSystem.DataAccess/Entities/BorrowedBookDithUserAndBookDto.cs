﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.Entities
{
    public class BorrowedBookDithUserAndBookDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

using LibraryManagementSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DataAccess.SQLite
{
    public class CategoryDal : GenericDal<Category>, ICategoryDal
    {
        public CategoryDal(SQLiteDbContext dbContext) : base(dbContext)
        {
        }
    }
}

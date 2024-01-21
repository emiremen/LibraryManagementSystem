using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryService(ICategoryDal categoryDal) : base(categoryDal)
        {
            _categoryDal = categoryDal;
        }
    }
}

using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        IUserDal _userDal;
        
        public UserService(IUserDal userDal) : base(userDal)
        {
            _userDal = userDal;
        }
    }
}

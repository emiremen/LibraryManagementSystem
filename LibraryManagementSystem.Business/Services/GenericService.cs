using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Business.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
    {
        protected IGenericDal<TEntity> _genericDal;

        public GenericService(IGenericDal<TEntity> genericDal)
        {
            _genericDal = genericDal;
        }

        public void Add(TEntity entity)
        {
            if (_genericDal.GetAll().Where(x => x.Name == entity.Name).Count() <= 0)
            {
                _genericDal.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            _genericDal.Delete(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _genericDal.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _genericDal.GetById(id);
        }

        public void Update(TEntity entity)
        {
            _genericDal.Update(entity);
        }
    }
}

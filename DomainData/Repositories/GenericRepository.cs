using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DomainData.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        protected readonly DbSet<TModel> _dbSet;

        public GenericRepository(DbContext context)
        {
            _dbSet = context.Set<TModel>();
        }
        public void Create(TModel entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);

        }
        public TModel GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IQueryable<TModel> GetAll()
        {
            return _dbSet.AsQueryable();
        }
    }
   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainData.Repositories
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        void Create(TModel entity);
        void Delete(int id);
        TModel GetById(int id);
        List<TModel> GetAll();
    }
}

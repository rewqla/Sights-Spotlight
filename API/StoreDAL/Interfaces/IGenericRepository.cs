using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FindById(int id);
        Task Add(TEntity entity);
        Task Delete(int id);
        Task Update(TEntity entity);
    }
}

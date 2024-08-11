using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDAL.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
        Task<TEntity> FindById(int id, CancellationToken cancellationToken = default);
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
        void Update(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> Complete();
        Task Dispose();
    }
}

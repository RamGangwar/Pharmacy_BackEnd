using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Repository
{
    public interface IGenericRepository<TEntity>  where TEntity : class
    {
        Task<int> Add(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetALL();
        Task<TEntity> GetById(Object id);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Func<TEntity, bool> condition);
        Task<TEntity> GetEntityAsync(Func<TEntity, bool> condition);
        Task<int> GetMaxValue(Func<TEntity, int> column, Func<TEntity, string> selector, string prefix);
    }
}

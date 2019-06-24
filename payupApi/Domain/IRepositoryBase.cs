using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace payupApi.Domain
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(T[] entities);
        Task SaveAsync();
        Task AddRangeAsync(T[] entities);
    }
}
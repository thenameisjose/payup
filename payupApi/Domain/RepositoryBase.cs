using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace payupApi.Domain
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext AppDbContext { get; set; }

        public RepositoryBase(AppDbContext dbContext)
        {
            this.AppDbContext = dbContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.AppDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.AppDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public async Task CreateAsync(T entity)
        {
            await this.AppDbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            this.AppDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.AppDbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(T[] entities)
        {
            this.AppDbContext.Set<T>().RemoveRange(entities);
        }

        public async Task AddRangeAsync(T[] entities)
        {
            await this.AppDbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task SaveAsync()
        {
            await this.AppDbContext.SaveChangesAsync();
        }
    }

}
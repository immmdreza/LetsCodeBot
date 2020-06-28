using LetsCodeApp.Data;
using LetsCodeApp.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LetsCodeApp.Repositories
{
    class Repository<T> : IRepository<T> where T : DbEntityBase
    {
        protected readonly ApplicationContext _applicationContext;

        public Repository()
        {
            _applicationContext = new ApplicationContext();
        }

        public async Task<int> Count()
        {
            return await _applicationContext.Set<T>().CountAsync();
        }

        public async Task<long> Create(T entity)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T> result = _applicationContext.Set<T>().Add(entity);

            await _applicationContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task Delete(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);

            await _applicationContext.SaveChangesAsync();
        }

        public async Task<T> GetById(long Id)
        {
            return await _applicationContext.Set<T>().FindAsync(Id);
        }

        public async Task Update(T entity)
        {
            _applicationContext.Set<T>().Update(entity);

            await _applicationContext.SaveChangesAsync();
        }
    }
}

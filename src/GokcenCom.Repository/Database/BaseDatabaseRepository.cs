using GokcenCom.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace GokcenCom.Repository.Database
{
    public abstract class BaseDatabaseRepository<T> where T : class
    {
        private readonly GokcenDBContext _dBContext;

        public BaseDatabaseRepository(GokcenDBContext context)
        {
            _dBContext = context;
        }

        public virtual void Add(T entity)
        {
            _dBContext.Set<T>().Add(entity);
            _dBContext.SaveChanges();
        }

        public virtual async Task InsertAllAsync(List<T> entities)
        {
            await _dBContext.BulkInsertAsync(entities);
        }

        public virtual async Task UpdateAsync()
        {
            await _dBContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dBContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entities = await _dBContext.Set<T>().ToListAsync();
            return entities;
        }
    }
}
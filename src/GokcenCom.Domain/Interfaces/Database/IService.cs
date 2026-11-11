namespace GokcenCom.Domain.Interfaces.Database
{
    public interface IService<T> where T : class
    {
        public void Add(T entity);
        public Task UpdateAsync(int id, T entity);
        public Task<T> GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
    }
}
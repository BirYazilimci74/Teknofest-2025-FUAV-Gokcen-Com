namespace GokcenCom.Domain.Interfaces.Server
{
    public interface IGETService<T> where T : class
    {
        Task<T> GetAsync();
    }
}
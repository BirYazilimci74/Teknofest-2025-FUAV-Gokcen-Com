namespace GokcenCom.Domain.Interfaces.Server
{
    public interface IPOSTService<T> where T : class
    {
        Task<bool> PostAsync(T data);
    }
}
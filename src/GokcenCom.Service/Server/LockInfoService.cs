using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class LockInfoService : IPOSTService<LockInfo>
    {
        private readonly BaseServerRepositoryPOST<LockInfo> _serverRepository;
        private readonly string _endPoint = "/api/kilitlenme_bilgisi";

        public LockInfoService(BaseServerRepositoryPOST<LockInfo> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<bool> PostAsync(LockInfo data)
        {
            try
            {
                var result = await _serverRepository.PostAsync(_endPoint,data);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Lock Pack to Server Exception: {ex.Message}");
                return false;
            }
            
        }
    }
}
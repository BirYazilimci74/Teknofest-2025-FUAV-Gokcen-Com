using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class KamikazeInfoService : IPOSTService<KamikazeInfo>
    {
        private readonly BaseServerRepositoryPOST<KamikazeInfo> _serverRepository;
        private readonly string _endPoint = "/api/kamikaze_bilgisi";
        
        public KamikazeInfoService(BaseServerRepositoryPOST<KamikazeInfo> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<bool> PostAsync(KamikazeInfo data)
        {
            try
            {
                var result = await _serverRepository.PostAsync(_endPoint, data);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Kamikaze Pack to Server Exception: {ex.Message}");
                return false;
            }
            
        }
    }
}
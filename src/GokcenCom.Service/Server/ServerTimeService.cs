using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class ServerTimeService : IGETService<ServerTime>
    {
        private readonly BaseServerRepositoryGET<ServerTime> _serverRepository;
        private readonly string _endPoint = "/api/sunucusaati";

        public ServerTimeService(BaseServerRepositoryGET<ServerTime> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<ServerTime> GetAsync()
        {
            try
            {
                var serverTime = await _serverRepository.GetAsync(_endPoint);
                return serverTime;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET Server Time from Server Exception: {ex.Message}");
                return null; 
            }

        }
    }
}
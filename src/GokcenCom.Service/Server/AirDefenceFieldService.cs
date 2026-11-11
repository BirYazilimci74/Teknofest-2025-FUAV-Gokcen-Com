using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class AirDefenceFieldService : IGETService<AirDefenceField>
    {
        private readonly BaseServerRepositoryGET<AirDefenceField> _serverRepository;
        private readonly string _endPoint = "/api/hss_koordinatlari";

        public AirDefenceFieldService(BaseServerRepositoryGET<AirDefenceField> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<AirDefenceField> GetAsync()
        {
            try
            {
                var hss = await _serverRepository.GetAsync(_endPoint);
                return hss;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET Air Defence Field from Server Exception: {ex.Message}");
                return null;
            }

        }
    }
}
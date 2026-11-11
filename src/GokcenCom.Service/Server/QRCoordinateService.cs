using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class QRCoordinateService : IGETService<QRCoordinate>
    {
        private readonly BaseServerRepositoryGET<QRCoordinate> _serverRepository;
        private readonly string _endPoint = "/api/qr_koordinati";

        public QRCoordinateService(BaseServerRepositoryGET<QRCoordinate> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<QRCoordinate> GetAsync()
        {
            try
            {
                var qrCoordinate = await _serverRepository.GetAsync(_endPoint);
                return qrCoordinate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET QR Coordinate from Server Exception: {ex.Message}");
                return null;
            }
            
        }
    }
}
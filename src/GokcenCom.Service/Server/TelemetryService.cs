using GokcenCom.Domain.Models;
using GokcenCom.Service.Mappers;
using GokcenCom.Service.Database;
using GokcenCom.Repository.Server;
using GokcenCom.Repository.Database;
using GokcenCom.Repository.DBContext;
using GokcenCom.Domain.Interfaces.Server;
using GokcenCom.Domain.Interfaces.Database;

namespace GokcenCom.Service.Server
{
    public class TelemetryService : IPOSTService<Telemetry>
    {
        private readonly IService<UAVLocationHistory> _locationHistoryService;
        private readonly IService<UAVCurrentLocation> _currentLocationService;
        private readonly BaseServerRepositoryPOST<Telemetry> _serverRepository;
        private readonly string _endPoint = "/api/telemetri_gonder";

        public TelemetryService(BaseServerRepositoryPOST<Telemetry> serverRepository)
        {
            _serverRepository = serverRepository;
            
            _locationHistoryService = new UAVLocationHistoryService(new UAVLocationHistoryRepository(new GokcenDBContext()));
            _currentLocationService = new UAVCurrentLocationService(new UAVCurrentLocationRepository(new GokcenDBContext()));
        }

        public async Task<bool> PostAsync(Telemetry data)
        {
            try
            {
                if (_serverRepository is TelemetrySR serverOperation && 
                    _locationHistoryService is UAVLocationHistoryService locationHistoryRepository &&
                    _currentLocationService is UAVCurrentLocationService currentLocationRepository)
                {
                    var uavList = await serverOperation.GetOtherUAVListAsync(_endPoint,data); //POST Telemetry
                    var uavLocationHistory = uavList.konumBilgileri.Select(uav => uav.toUAVLocationHistory()).ToList();
                    await locationHistoryRepository.InsertAllAsync(uavLocationHistory);

                    foreach (var location in uavList.konumBilgileri)
                    {
                        var uav = await _currentLocationService.GetByIdAsync(location.takim_numarasi);
                        if (uav is null)
                        {
                            _currentLocationService.Add(location.toUAVCurrentLocation());
                            continue;
                        }
                        await _currentLocationService.UpdateAsync(uav.takim_numarasi,location.toUAVCurrentLocation());
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Telemetry to Server to get Other UAV Locations Exception: {ex.Message}");
                return false;
            }

            var result = await _serverRepository.PostAsync(_endPoint,data);            
            return result;
        }
    }
}
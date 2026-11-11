using GokcenCom.Domain.Models;
using GokcenCom.Repository.Database;
using GokcenCom.Domain.Interfaces.Database;

namespace GokcenCom.Service.Database
{
    public class UAVCurrentLocationService : IService<UAVCurrentLocation>
    {
        private readonly BaseDatabaseRepository<UAVCurrentLocation> _databaseRepository;

        public UAVCurrentLocationService(BaseDatabaseRepository<UAVCurrentLocation> databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public void Add(UAVCurrentLocation entity)
        {
            try
            {
                _databaseRepository.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Adding to database UAVCurrentLocation Exception: {ex.Message}");
            }
        }

        public async Task<List<UAVCurrentLocation>> GetAllAsync()
        {
            try
            {
                var uavCurrentLocations = await _databaseRepository.GetAllAsync();
                return uavCurrentLocations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Getting all UAVCurrentLocation Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<UAVCurrentLocation> GetByIdAsync(int id)
        {
            try
            {
                var uavCurrentLocation = await _databaseRepository.GetByIdAsync(id);
                return uavCurrentLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Getting UAVCurrentLocation by id Exception: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateAsync(int id, UAVCurrentLocation updatedEntity)
        {
            try
            {
                var uavToUpdate = await GetByIdAsync(id);
                uavToUpdate.iha_enlem = updatedEntity.iha_enlem;
                uavToUpdate.iha_boylam = updatedEntity.iha_boylam;
                uavToUpdate.iha_irtifa = updatedEntity.iha_irtifa;
                uavToUpdate.iha_dikilme = updatedEntity.iha_dikilme;
                uavToUpdate.iha_yonelme = updatedEntity.iha_yonelme;
                uavToUpdate.iha_yatis = updatedEntity.iha_yatis;
                uavToUpdate.iha_hizi = updatedEntity.iha_hizi;
                uavToUpdate.zaman_farki = updatedEntity.zaman_farki;

                await _databaseRepository.UpdateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Updating UAVCurrentLocation Exception: {ex.Message}");
            }
        }
    }
}
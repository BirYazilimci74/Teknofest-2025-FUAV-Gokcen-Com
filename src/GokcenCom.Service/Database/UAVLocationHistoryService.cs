using GokcenCom.Domain.Models;
using GokcenCom.Repository.Database;
using GokcenCom.Domain.Interfaces.Database;

namespace GokcenCom.Service.Database
{
    public class UAVLocationHistoryService : IService<UAVLocationHistory>
    {
        private readonly BaseDatabaseRepository<UAVLocationHistory> _databaseRepository;

        public UAVLocationHistoryService(BaseDatabaseRepository<UAVLocationHistory> databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public void Add(UAVLocationHistory entity)
        {
            try
            {
                _databaseRepository.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Adding to database UAVLocationHistory Exception: {ex.Message}");
            }
            
        }

        public async Task InsertAllAsync(List<UAVLocationHistory> entities)
        {
            try
            {
                await _databaseRepository.InsertAllAsync(entities);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inserting to database all UAVLocationHistories Exception: {ex.Message}");
            }
        }

        public async Task<List<UAVLocationHistory>> GetAllAsync()
        {
            try
            {
                var uavLocationHistories = await _databaseRepository.GetAllAsync();
                return uavLocationHistories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Getting all UAVLocationHistories Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<UAVLocationHistory> GetByIdAsync(int id)
        {
            try
            {
                var uavLocationHistory =  await _databaseRepository.GetByIdAsync(id);
                return uavLocationHistory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Getting UAVLocationHistory bt id Exception: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateAsync(int id, UAVLocationHistory entity)
        {
            try
            {
                await _databaseRepository.UpdateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Updating UAVLocationHistory Exception: {ex.Message}");
            }
        }
    }
}
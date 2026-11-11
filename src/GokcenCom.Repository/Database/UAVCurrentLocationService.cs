using GokcenCom.Domain.Models;
using GokcenCom.Repository.DBContext;

namespace GokcenCom.Repository.Database
{
    public class UAVCurrentLocationRepository : BaseDatabaseRepository<UAVCurrentLocation>
    {
        public UAVCurrentLocationRepository(GokcenDBContext context) : base(context)
        {
        }
    }
}
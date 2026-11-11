using GokcenCom.Domain.Models;
using GokcenCom.Repository.DBContext;

namespace GokcenCom.Repository.Database
{
    public class UAVLocationHistoryRepository : BaseDatabaseRepository<UAVLocationHistory>
    {
        public UAVLocationHistoryRepository(GokcenDBContext context) : base(context)
        {
        }
    }
}
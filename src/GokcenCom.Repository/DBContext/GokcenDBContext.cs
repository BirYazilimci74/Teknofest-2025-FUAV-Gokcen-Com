using GokcenCom.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GokcenCom.Repository.DBContext
{
    public class GokcenDBContext : DbContext
    {
        public DbSet<UAVCurrentLocation> UAVCurrentLocations { get; set; }
        public DbSet<UAVLocationHistory> UAVLocationHistorys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=GokcenDB;Username=postgres;Password=postgres");
        }
    }
}
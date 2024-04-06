using MaintenanceLog.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog.DataAccess
{
    public class MaintenanceLogContext : DbContext
    {
        public MaintenanceLogContext(DbContextOptions<MaintenanceLogContext> options) : base(options) { }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

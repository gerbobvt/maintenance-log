using MaintenanceLog.DataAccess;
using MaintenanceLog.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceLog
{
    public static class DbUtils
    {
        public static async Task EnsureDbCreated(DbContextOptions<MaintenanceLogContext> options)
        {
            var builder = new DbContextOptionsBuilder<MaintenanceLogContext>(options)
                .UseLoggerFactory(new LoggerFactory());

            using var context = new MaintenanceLogContext(builder.Options);

            if (await context.Database.EnsureCreatedAsync())
            {
                await AddTestVehicles(context);
            }
        }

        public static async Task AddTestVehicles(MaintenanceLogContext context)
        {
            var xterra = new Vehicle { Make = "Nissan", Model = "XTerra", Name = "Bouncy", Year = 2012 };
            var ion = new Vehicle { Make = "Saturn", Model = "Ion", Name = "Wheezy", Year = 2007 };

            context.Vehicles.Add(xterra);
            context.Vehicles.Add(ion);

            await context.SaveChangesAsync();
        }
    }
}

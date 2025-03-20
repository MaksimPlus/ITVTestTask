using ITVTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ITVTestTask.Data
{
    public class DeviceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=app.db");
            }
        }
        public DbSet<Device> Devices { get; set; }
    }
}

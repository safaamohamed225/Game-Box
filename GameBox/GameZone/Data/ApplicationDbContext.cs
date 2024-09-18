//using Microsoft.EntityFrameworkCore;
using GameBox.Models;
using Microsoft.Data;
namespace GameBox.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category{Id=1 , Name="Sports"},
                new Category{Id=2 , Name="Action"},
                new Category{Id=3 , Name="Boxing"},
                new Category{Id=4 , Name="Adventure"},
                new Category{Id=5 , Name="Fighting"}

            });

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                new Device{Id=1 , Name="Pc" , Icon="bi bi-pc-display-horizontal"},
                new Device{Id=2 , Name="Tablet" , Icon="bi bi-tablet"},
                new Device{Id=3 , Name="Mobile" , Icon = "bi bi-phone"},
                new Device{Id=4 , Name="PlayStation" , Icon="bi bi-playstation"}
            });

            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.DeviceId, e.GameId });
            base.OnModelCreating(modelBuilder);
        }
    }
}

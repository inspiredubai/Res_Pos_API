
using InspirePO.Models;
using Microsoft.EntityFrameworkCore;


namespace InspirePO.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<RestOutTableMaster> RestOutTableMasters { get; set; }
        public DbSet<FormCategory> FormCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestOutTableMaster>()
                .ToTable("Rest_OutTableMaster"); // Ensure correct table mapping
        }
    }
}

using CarShopAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Data
{
  public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
        .HasMany(u => u.RecommendedCars)
        .WithMany(c => c.Users)
        .UsingEntity(j => j.ToTable("Recommendation"));

      modelBuilder.Entity<User>()
        .HasData(
            new User(1, "captain", "Samir", "Ahmed", "pass1234", true)
          );

      base.OnModelCreating(modelBuilder);
    }
  }
}

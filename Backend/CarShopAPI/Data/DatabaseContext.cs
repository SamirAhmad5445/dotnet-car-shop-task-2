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
            new User(1, "captain", "Samir", "Ahmad", "captain123", true),
            new User(2, "luffy", "Monkey D.", "Luffy", "pass1234", false),
            new User(3, "zoro", "Roronoa", "Zoro", "pass1234", false),
            new User(4, "sanji", "Vinsmoke", "Sanji", "pass1234", false),
            new User(5, "usopp", "Captain", "Usopp", "pass1234", false),
            new User(6, "nami", "Bellymare", "Nami", "pass1234", false),
            new User(7, "chopper", "Tony Tony", "chopper", "pass1234", false),
            new User(8, "robin", "Nico", "Robin", "pass1234", false),
            new User(9, "franky", "Cutty Flam", "Franky", "pass1234", false),
            new User(10, "brook", "Soul King", "Brook", "pass1234", false),
            new User(11, "jinbei", "Jinbe", "Jinbei", "pass1234", false)
         );

      modelBuilder.Entity<Car>()
        .HasData(
          new Car(1, "Sunny", 2009, "Red", "Gas"),
          new Car(2, "Tesla Model S", 2012, "Red", "Electrical"),
          new Car(3, "Nissan Leaf", 2010, "Green", "Electrical"),
          new Car(4, "Ford Mustang", 2005, "Yellow", "Gas"),
          new Car(5, "Honda Civic", 2001, "Silver", "Gas"),
          new Car(6, "Chevrolet Corvette", 2005, "Black", "Gas"),
          new Car(7, "BMW X5", 2000, "White", "Gas"),
          new Car(8, "Mercedes-Benz E-Class", 2002, "Gray", "Gas"),
          new Car(9, "Audi Q7", 2005, "Blue", "Gas"),
          new Car(10, "Subaru Outback", 2000, "Green", "Gas"),
          new Car(11, "Volkswagen Golf", 2003, "Red", "Gas"),
          new Car(12, "Lexus RX", 2004, "Silver", "Gas"),
          new Car(13, "Jeep Wrangler", 2007, "Orange", "Gas"),
          new Car(14, "Mazda CX-5", 2012, "Blue", "Gas"),
          new Car(15, "Volvo XC90", 2003, "Black", "Gas")
        );

      base.OnModelCreating(modelBuilder);
    }
  }
}

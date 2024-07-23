using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CarShopAPI.Model
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public bool IsAdmin { get; set; } = false;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public bool IsActive { get; set; } = false;

    public List<Car>? RecommendedCars { get; set; } = [];

    public User(int Id, string Username, string FirstName, string LastName, string Password, bool IsAdmin = false)
    {
      this.Id = Id;
      this.Username = Username;
      this.FirstName = FirstName;
      this.LastName = LastName;
      this.IsAdmin = IsAdmin;
      this.IsActive = IsAdmin;
      UpdatePassword(Password);
    }

    public User(int Id, string Username, string FirstName, string LastName, byte[] PasswordHash, byte[] PasswordSalt, bool IsActive)
    {
      this.Id = Id;
      this.Username = Username;
      this.FirstName = FirstName;
      this.LastName = LastName;
      this.PasswordHash = PasswordHash;
      this.PasswordSalt = PasswordSalt;
      this.IsActive = IsActive;
    }
    
    public User(string Username, string FirstName, string LastName, string Password)
    {
      this.Id = Id;
      this.Username = Username;
      this.FirstName = FirstName;
      this.LastName = LastName;
      UpdatePassword(Password);
    }


    private static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
    {
      using var hmac = new HMACSHA512();
      PasswordSalt = hmac.Key;
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
    }

    public bool VerifyPassword(string Password)
    {
      using var hmac = new HMACSHA512(PasswordSalt);
      byte[] ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
      return ComputedHash.SequenceEqual(PasswordHash);
    }

    public void UpdatePassword(string Password)
    {
      CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

      this.PasswordHash = PasswordHash;
      this.PasswordSalt = PasswordSalt;
    }
  }
}

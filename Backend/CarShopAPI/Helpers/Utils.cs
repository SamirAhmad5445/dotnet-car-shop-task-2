using CarShopAPI.Model;
using Newtonsoft.Json;

namespace CarShopAPI.Helpers
{
  public static class Utils
  {
    private static string GetAdminFile()
    {
      return File.ReadAllText("Json/admin.json");
    }

    private static string GetUsersFile()
    {
      return File.ReadAllText("Json/users.json");
    }

    private static string GetCarsFile()
    {
      return File.ReadAllText("Json/cars.json");
    }

    public static void UpdateUsersFile(List<User> Users)
    {
      File.WriteAllText("Json/users.json", JsonConvert.SerializeObject(Users));
    }

    public static void UpdateCarsFile(List<Car> Cars)
    {
      File.WriteAllText("Json/cars.json", JsonConvert.SerializeObject(Cars));
    }

    public static User GetAdminUser()
    {
      return JsonConvert.DeserializeObject<User>(GetAdminFile())!;
    }

    public static List<User> GetAllUsers()
    {
      return JsonConvert.DeserializeObject<List<User>>(GetUsersFile())!;
    }

    public static User? GetUser(string Username)
    {
      if (Username == null || Username == string.Empty)
      {
        return null;
      }

      return GetAllUsers().FirstOrDefault(u => u.Username == Username);
    }
    public static List<Car> GetAllCars()
    {
      return JsonConvert.DeserializeObject<List<Car>>(GetCarsFile())!;
    }

    public static Car? GetCar(int CarId)
    {
      if (CarId < 0 || CarId >= GetAllCars().Count)
      {
        return null;
      }

      return GetAllCars().FirstOrDefault(c => c.Id == CarId);
    }

    public static Car? GetCarByName(string Name)
    {
      if (Name == null || Name == string.Empty)
      {
        return null;
      }

      return GetAllCars().FirstOrDefault(c => c.Name == Name);
    }
  }
}

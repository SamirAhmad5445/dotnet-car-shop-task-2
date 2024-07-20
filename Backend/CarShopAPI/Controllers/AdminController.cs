using CarShopAPI.Helpers;
using CarShopAPI.Model;
using CarShopAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  [Administrator]
  public class AdminController : ControllerBase
  {
    [HttpGet("user/all")]
    public ActionResult<List<User>> GetAllUsers()
    {
      return Ok(Utils.GetAllUsers());
    }

    [HttpPost("user/add")]
    public ActionResult<string> AddUser(UserRequestDTO request)
    {
      if (request.Username == null || request.Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      if (request.FirstName == null || request.FirstName == string.Empty)
      {
        return BadRequest("Invalid user first name.");
      }

      if (request.LastName == null || request.LastName == string.Empty)
      {
        return BadRequest("Invalid user last name.");
      }

      if(Utils.GetUser(request.Username) != null)
      {
        return Conflict("Username is already taken.");
      }

      List<User> Users = Utils.GetAllUsers();
      Users.Add(new User()
      {
        Id = Users.Count,
        Username = request.Username,
        FirstName = request.FirstName,
        LastName = request.LastName,
        RecommendedCars = []
      });

      System.IO.File.WriteAllText("Json/users.json", JsonConvert.SerializeObject(Users));

      return Ok("New user added successfully.");
    }

    [HttpGet("car/all")]
    public ActionResult<List<Car>> GetAllCars()
    {
      return Ok(Utils.GetAllCars());
    }

    [HttpPost("car/add")]
    public ActionResult<string> AddCar(CarRequestDTO request)
    {
      if (request.Name == null || request.Name == string.Empty)
      {
        return BadRequest("Invalid car name.");
      }

      if (request.ModelNumber == null || request.ModelNumber > DateTime.Now.Year)
      {
        return BadRequest("Invalid car name.");
      }

      if (request.Color == null || request.Color == string.Empty)
      {
        return BadRequest("Invalid car color.");
      }

      if (request.Type == null || request.Type == string.Empty)
      {
        return BadRequest("Invalid car type.");
      }

      if (Utils.GetCarByName(request.Name) != null)
      {
        return Conflict("The car already exists.");

      }

      List<Car> Cars = Utils.GetAllCars();
      Cars.Add(new Car()
      {
        Id = Cars.Count,
        Name = request.Name,
        ModelNumber = request.ModelNumber,
        Color = request.Color,
        Type = request.Type,
      });

      Utils.UpdateCarsFile(Cars);
      return Ok("New Car added successfully");
    }

    [HttpPost("recommend")]
    public ActionResult<string> Recommend(RecommendRequestDTO request)
    {
      if (Utils.GetUser(request.Username) == null)
      {
        return BadRequest("Invalid username.");
      }

      if (request.RecommendedCars == null || request.RecommendedCars.Count == 0)
      {
        return BadRequest("Invalid recommendation array.");
      }

      List<int> RecommendedCars = request.RecommendedCars;
      RecommendedCars.RemoveAll(c => c < 0 || c > Utils.GetAllCars().Count);

      HashSet<int> CarsSet = new HashSet<int>(RecommendedCars);

      List<User> Users = Utils.GetAllUsers();
      Users.Find(u => u.Username == request.Username)!.RecommendedCars = CarsSet.ToList();

      Utils.UpdateUsersFile(Users);
      return Ok("The Recommendation added successfully");
    }
  }
}

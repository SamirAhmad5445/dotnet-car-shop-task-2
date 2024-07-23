using CarShopAPI.Data;
using CarShopAPI.Model;
using CarShopAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShopAPI.Helpers;


namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  [LoggedOnly]
  public class AdminController(DatabaseContext db) : ControllerBase
  {
    [HttpGet("user/all")]
    public ActionResult<List<UserDTO>> GetAllUsers()
    {
      if(!IsAdmin())
      {
        return Unauthorized("your access has been denied.");
      }

      List<User> Users = [.. db.Users];
      List<UserDTO> response = [];

      foreach(User u in Users)
      {
        if (u.IsAdmin) {
          continue;
        }

        response.Add(new UserDTO()
        {
          Username = u.Username,
          FirstName = u.FirstName,
          LastName = u.LastName,
          IsActive = u.IsActive
        });
      }

      return Ok(response);
    }

    [HttpPost("user/add")]
    public ActionResult<string> AddUser(AddUserDTO request)
    {
      if (!IsAdmin())
      {
        return Unauthorized("your access has been denied.");
      }

      if (request.Username == null || request.Username == string.Empty)
      {
        return BadRequest("Expecting username for creating new user.");
      }

      if (request.FirstName == null || request.FirstName == string.Empty)
      {
        return BadRequest("Expecting first name for the new user.");
      }

      if (request.LastName == null || request.LastName == string.Empty)
      {
        return BadRequest("Expecting last name for the new user.");
      }

      if (request.Password == null || request.Password == string.Empty || request.Password.Length < 6)
      {
        return BadRequest("Expecting password for creating new user.");
      }

      User? User = db.Users.FirstOrDefault(u => u.Username == request.Username);

      if (User != null)
      {
        return Conflict("Username is already exist.");
      }

      User NewUser = new(request.Username, request.FirstName, request.LastName, request.Password);

      db.Users.Add(NewUser);
      db.SaveChanges();
      return Ok("New user added successfully.");
    }

    [HttpGet("car/all")]
    public ActionResult<List<CarDTO>> GetAllCars()
    {
      if (!IsAdmin())
      {
        return Unauthorized("your access has been denied.");
      }

      List<Car> Cars = [.. db.Cars];
      List<CarDTO> response = [];

      foreach (Car c in Cars)
      {
        response.Add(new CarDTO()
        {
          Name = c.Name,
          ModelNumber = c.ModelNumber,
          Color = c.Color,
          Type = c.Type
        });
      }

      return Ok(response);
    }

    [HttpPost("car/add")]
    public ActionResult<string> AddCar(CarDTO request)
    {
      if (!IsAdmin())
      {
        return Unauthorized("your access has been denied.");
      }

      if (request.Name == null || request.Name == string.Empty)
      {
        return BadRequest("Expecting name for creating new car.");
      }

      if (request.ModelNumber > DateTime.Now.Year)
      {
        return BadRequest("Expecting model number for the new car.");
      }

      if (request.Color == null || request.Color == string.Empty)
      {
        return BadRequest("Expecting color for the new car.");
      }

      if (request.Type == null || request.Type == string.Empty)
      {
        return BadRequest("Expecting type for the new car.");
      }

      Car NewCar = new(request.Name, request.ModelNumber, request.Color, request.Type);

      db.Cars.Add(NewCar);
      db.SaveChanges();
      return Ok("New Car added successfully");
    }

    [HttpPost("recommend")]
    public ActionResult<string> Recommend(RecommendRequestDTO request)
    {
      if (!IsAdmin())
      {
        return Unauthorized("your access has been denied.");
      }

      if (request.Username == null || request.Username == string.Empty)
      {
        return BadRequest("Expecting username to make a reacommendation.");
      }

      if (request.RecommendedCars == null || request.RecommendedCars.Count == 0)
      {
        return BadRequest("Expecting recommendation array.");
      }

      var User = db.Users
        .Include(u => u.RecommendedCars)
        .SingleOrDefault(u => u.Username == request.Username);


      if (User == null)
      {
        return NotFound("User is not found.");
      }

      List<Car> RecommendedCars = [.. db.Cars.Where(c => request.RecommendedCars.Contains(c.Id))];

      User.RecommendedCars!.Clear();
      User.RecommendedCars.AddRange(RecommendedCars);

      db.SaveChanges();
      return Ok("The Recommendation added successfully");
    }

    private bool IsAdmin()
    {
      string? Username = Request.Cookies["username"];
      string? Role = Request.Cookies["role"];

      if (Username == null || Username == string.Empty)
      {
        return false;
      }

      if (Role == null || Role == string.Empty)
      {
        return false;
      }

      User? User = db.Users.SingleOrDefault(u => u.Username == Username);

      if (User == null)
      {
        return false;
      }

      return Role == "admin" && User.IsAdmin;
    }
  }
}

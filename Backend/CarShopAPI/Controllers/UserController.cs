using CarShopAPI.Helpers;
using CarShopAPI.Model;
using CarShopAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  [LoggedOnly]
  public class UserController : ControllerBase
  {
    [HttpGet("user/info")]
    public ActionResult<UserRequestDTO> GetUserInfo()
    {
      var Username = Request.Cookies["username"]!;

      if (Username == null || Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      User? User = Utils.GetUser(Username);

      if (User == null)
      {
        return NotFound("User doesn't exist.");
      }

      return Ok(new UserRequestDTO() {
        Username = User.Username,
        LastName = User.LastName,
        FirstName = User.FirstName
      });
    }

    [HttpGet("user/recommended")]
    public ActionResult<List<CarRequestDTO>> GetRecommendedCars()
    {
      var Username = Request.Cookies["username"]!;

      if (Username == null || Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      User? User = Utils.GetUser(Username);

      if (User == null)
      {
        return NotFound("User doesn't exist.");
      }

      List<CarRequestDTO> RecommendedCars = [];

      foreach(int id in User.RecommendedCars)
      {
        Car? Car = Utils.GetCar(id);
        if (Car != null)
        {
          RecommendedCars.Add(new CarRequestDTO()
          {
            Name = Car.Name,
            ModelNumber = Car.ModelNumber,
            Color = Car.Color,
            Type = Car.Type,
          });
        }
      }

      return Ok(RecommendedCars);
    }
  }
}

using Azure.Core;
using CarShopAPI.Data;
using CarShopAPI.Helpers;
using CarShopAPI.Model;
using CarShopAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  [LoggedOnly]
  public class UserController(DatabaseContext db) : ControllerBase
  {
    [HttpGet("user/info")]
    public ActionResult<UserDTO> GetUserInfo()
    {
      string Username = Request.Cookies["username"]!;

      if (Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      User? User = db.Users.FirstOrDefault(u => u.Username == Username);

      if (User == null)
      {
        return NotFound("User is not found.");
      }

      if (!User.IsActive)
      {
        return Unauthorized("Please activate the account by changing your default password.");
      }

      return Ok(new UserDTO()
      {
        Username = User.Username,
        FirstName = User.FirstName,
        LastName = User.LastName,
        IsActive = User.IsActive
      });
    }

    [HttpGet("user/recommended")]
    public ActionResult<List<CarDTO>> GetRecommendedCars()
    {
      string Username = Request.Cookies["username"]!;

      if (Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      var User = db.Users
        .Include(u => u.RecommendedCars)
        .SingleOrDefault(u => u.Username == Username);

      if (User == null)
      {
        return NotFound("User doesn't exist.");
      }

      if (!User.IsActive)
      {
        return Unauthorized("Please activate the account by changing your default password.");
      }

      List<CarDTO> RecommendedCars = [];

      foreach (Car Car in User.RecommendedCars!)
      {
        RecommendedCars.Add(new CarDTO()
        {
          Name = Car.Name,
          ModelNumber = Car.ModelNumber,
          Color = Car.Color,
          Type = Car.Type,
        });
      }

      return Ok(RecommendedCars);
    }

    [HttpPut("user/activate")]
    public ActionResult<string> ActivateAccount([FromBody]string NewPassword)
    {
      string Username = Request.Cookies["username"]!;

      if (Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      User? User = db.Users.FirstOrDefault(u => u.Username == Username);

      if (User == null)
      {
        return NotFound("User doesn't exist.");
      }

      if (User.IsActive)
      {
        return Conflict("Your account has been activated before.");
      }

      if (NewPassword == null || NewPassword == string.Empty || NewPassword.Length < 8)
      {
        return BadRequest("Invalide password, please enter another pasword to activate the account.");
      }

      if (User.VerifyPassword(NewPassword))
      {
        return BadRequest("Your new password can't be the old password");
      }

      User.UpdatePassword(NewPassword);
      User.IsActive = true;

      db.Users.Update(User);
      db.SaveChanges();

      return Ok("Your account has been activated.");
    }
  }
}

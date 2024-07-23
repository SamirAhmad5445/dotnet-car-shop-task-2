using CarShopAPI.Data;
using CarShopAPI.Helpers;
using CarShopAPI.Model;
using CarShopAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  public class AuthController(DatabaseContext db) : ControllerBase
  {
    [HttpPost("login")]
    public ActionResult<LoginResponseDTO> UserLogin(LoginRequestDTO request)
    {
      if (request.Username == null || request.Username == string.Empty)
      {
        return BadRequest("Username is required to login.");
      }

      if (request.Password == null || request.Password == string.Empty)
      {
        return BadRequest("A password is required to login.");
      }

      User? User = db.Users.FirstOrDefault(u => u.Username == request.Username);

      if (User == null || !User!.VerifyPassword(request.Password))
      {
        DeleteCookies();
        return BadRequest("Invalid username or password.");
      }

      string Role = User.IsAdmin ? "admin" : "user";
      var response = new LoginResponseDTO
      {
        Username = User.Username,
        Role = Role,
        IsActive = User.IsActive
      };

      CreateCookies(User.Username, Role);
      return Ok(response);
    }

    private void CreateCookies(string Username, string Role)
    {
      var CookieOpt = new CookieOptions()
      {
        Secure = true,
        HttpOnly = false,
        SameSite = SameSiteMode.None,
      };

      Response.Cookies.Append("username", Username, CookieOpt);
      Response.Cookies.Append("role", Role, CookieOpt);
    }

    private void DeleteCookies()
    {
      Response.Cookies.Delete("username");
      Response.Cookies.Delete("role");
    }
  }
}

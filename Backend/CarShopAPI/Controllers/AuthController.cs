using CarShopAPI.Helpers;
using CarShopAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Controllers
{
  [Route("api")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    [HttpPost("login/user")]
    public ActionResult<string> UserLogin([FromBody]string Username)
    {
      if (Username == null || Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      User? User = Utils.GetUser(Username);

      if (User == null)
      {
        CreateCookie("", "");
        return NotFound("Username doesn't exist.");
      }

      CreateCookie(Username, "user");
      return Ok($"Welcome, {User.Username}");
    }

    [HttpPost("login/admin")]
    public ActionResult<string> AdminLogin([FromBody]string Username)
    {
      if (Username == null || Username == string.Empty)
      {
        return BadRequest("Invalid username.");
      }

      if (Username != Utils.GetAdminUser().Username)
      {
        CreateCookie("", "");
        return Unauthorized("Invalid admin user.");
      }

      CreateCookie(Username, "admin");
      return Ok($"Welcome, {Utils.GetAdminUser().Username} (admin).");
    }

    private void CreateCookie(string Username, string Role)
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
  }
}

namespace CarShopAPI.Models.DTO
{
  public class LoginRequestDTO
  {
    public required string Username { get; set; }
    public required string Password { get; set; }
  }
}

namespace CarShopAPI.Models.DTO
{
  public class LoginResponseDTO
  {
    public required string Username { get; set; }
    public required string Role { get; set; }
    public required bool IsActive { get; set; }
  }
}

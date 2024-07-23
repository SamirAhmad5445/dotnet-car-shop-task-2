namespace CarShopAPI.Models.DTO
{
  public class UserDTO
  {
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required bool IsActive {  get; set; }
  }
}

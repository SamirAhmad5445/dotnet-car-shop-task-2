namespace CarShopAPI.Models.DTO
{
  public class RecommendRequestDTO
  {
    public required string Username { get; set; }
    public required List<int> RecommendedCars { get; set; }
  }
}

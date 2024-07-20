namespace CarShopAPI.Model
{
  public class User
  {
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required List<int> RecommendedCars { get; set; } = [];
  }
}

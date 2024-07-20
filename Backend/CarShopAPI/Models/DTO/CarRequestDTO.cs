namespace CarShopAPI.Models.DTO
{
  public class CarRequestDTO
  {
    public required string Name { get; set; }
    public required int ModelNumber { get; set; }
    public required string Color { get; set; }
    public required string Type { get; set; }
  }
}

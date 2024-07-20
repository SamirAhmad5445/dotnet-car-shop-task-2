namespace CarShopAPI.Model
{
  public class Car
  {
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int ModelNumber { get; set; }
    public required string Color { get; set; }
    public required string Type { get; set; }
  }
}

namespace CarShopAPI.Model
{
  public class Car
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int ModelNumber { get; set; }
    public string Color { get; set; }
    public string Type { get; set; }
    public List<User> Users { get; set; } = [];

    public Car(string name, int modelNumber, string color, string type)
    {
      Name = name;
      ModelNumber = modelNumber;
      Color = color;
      Type = type;
    }

    public Car(int id, string name, int modelNumber, string color, string type, List<User> users)
    {
      Id = id;
      Name = name;
      ModelNumber = modelNumber;
      Color = color;
      Type = type;
      Users = users;
    }
  }
}

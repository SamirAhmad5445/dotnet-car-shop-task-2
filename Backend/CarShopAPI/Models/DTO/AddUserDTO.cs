﻿namespace CarShopAPI.Models.DTO
{
  public class AddUserDTO
  {
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
  }
}
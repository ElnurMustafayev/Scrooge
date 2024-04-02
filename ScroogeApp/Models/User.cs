namespace ScroogeApp.Models;

public class User
{
    public int Id { get; set; }
    public string Address => $"<a href=\"/Users/GetUser?Id={Id}\">Details</a>";
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? BirthDate { get; set; }
}
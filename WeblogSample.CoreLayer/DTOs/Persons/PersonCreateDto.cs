namespace WeblogSample.Service.DTOs.Persons;

public class PersonCreateDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public short? RoleId { get; set; }
}

namespace WeblogSample.Service.DTOs.Persons;

public class PersonUpdateDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public short? RoleId { get; set; }
}
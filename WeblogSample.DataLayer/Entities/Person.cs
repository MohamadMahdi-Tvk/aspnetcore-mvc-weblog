namespace WeblogSample.Data.Entities;

public class Person : BaseEntity
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public short? RoleId { get; set; }
    public Role Role { get; set; }
    public List<Article> Articles { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}

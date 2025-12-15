namespace WeblogSample.Data.Entities;

public class Category
{
    public short Id { get; set; }
    public string Name { get; set; }
    public List<Article> Articles { get; set; } = new();
}

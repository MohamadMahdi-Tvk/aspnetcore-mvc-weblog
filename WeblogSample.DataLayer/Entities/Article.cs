namespace WeblogSample.Data.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }

    public int VisitCount { get; set; } = 0;
    public long? PersonId { get; set; }
    public Person Person { get; set; }

    public short? CategoryId { get; set; }
    public Category Category { get; set; }

    public List<Comment> Comments { get; set; } = new();

}

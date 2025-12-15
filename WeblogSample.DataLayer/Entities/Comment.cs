namespace WeblogSample.Data.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public long ArticleId { get; set; }
    public Article Article { get; set; }

    public long? PersonId { get; set; }
    public Person Person { get; set; }

    public long? ParentId { get; set; }
    public Comment Parent { get; set; }

    public ICollection<Comment>? Replies { get; set; } = new List<Comment>();

    public bool IsApproved { get; set; } = false;
}
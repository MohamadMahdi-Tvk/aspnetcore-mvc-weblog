namespace WeblogSample.Service.DTOs.Comments;

public class CommentCreateDto
{
    public string Text { get; set; }
    public long? PersonId { get; set; }
    public long ArticleId { get; set; }
    public long? ParentId { get; set; }

}

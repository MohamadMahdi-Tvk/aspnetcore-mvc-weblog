namespace WeblogSample.Service.DTOs.Comments;

public class CommentReplayDto
{
    public string Text { get; set; }
    public long? PersonId { get; set; }
    public long ArticleId { get; set; }
    public long? ParentId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }

}
using WeblogSample.Service.DTOs.Comments;

namespace WeblogSample.Service.DTOs.Articles;

public class ArticleDetailDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public int VisitCount { get; set; }
    public string AuthorName { get; set; }

    public short? CategoryId { get; set; }

    public long PersonId { get; set; }
    public string CategoryName { get; set; }

    public List<CommentDto> Comments { get; set; } = new();
    public DateTime InsertDate { get; set; }
}

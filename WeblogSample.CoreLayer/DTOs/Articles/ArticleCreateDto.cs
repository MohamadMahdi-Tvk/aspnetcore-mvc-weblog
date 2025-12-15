namespace WeblogSample.Service.DTOs.Articles;

public class ArticleCreateDto
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string? ImagePath { get; set; }
    public int VisitCount { get; set; }
    public long? PersonId { get; set; }
    public short? CategoryId { get; set; }

}

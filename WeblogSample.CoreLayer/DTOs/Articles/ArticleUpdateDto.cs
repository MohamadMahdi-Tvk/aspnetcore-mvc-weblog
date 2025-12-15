namespace WeblogSample.Service.DTOs.Articles;

public class ArticleUpdateDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string? ImagePath { get; set; }
    public short? CategoryId { get; set; }
    public long? PersonId { get; set; }
}
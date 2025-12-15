using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WeblogSample.Service.DTOs.Articles;
using WeblogSample.Service.Extentions;

namespace WeblogSample.WebApp.Infra.PdfGenerator;

public interface IArticlePdfGenerator
{
    byte[] Generate(ArticleDetailDto article);
}

public class QuestArticlePdfGenerator : IArticlePdfGenerator
{
    private readonly IWebHostEnvironment _env;

    public QuestArticlePdfGenerator(IWebHostEnvironment env)
    {
        _env = env;
    }
    public byte[] Generate(ArticleDetailDto article)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Content().Column(column =>
                {
                    // عنوان مقاله
                    column.Item().Text(FixRtlText(article.Title))
                        .FontSize(18)
                        .Bold()
                        .AlignCenter()
                        .DirectionFromRightToLeft()
                        .LineHeight(1.3f);

                    column.Item().PaddingVertical(8);

                    // اطلاعات مقاله
                    column.Item().Text(
                        $"نویسنده: {article.AuthorName}   |   تاریخ: {article.InsertDate.ToShamsi()}   |   موضوع: {article.CategoryName}"
                    )
                    .FontSize(10)
                    .AlignCenter()
                    .DirectionFromRightToLeft();

                    column.Item().PaddingVertical(15);

                    // تصویر مقاله
                    if (!string.IsNullOrEmpty(article.ImagePath))
                    {
                        var imagePath = Path.Combine(
                            _env.WebRootPath,
                            article.ImagePath.TrimStart('/')
                        );

                        if (File.Exists(imagePath))
                        {
                            column.Item()
                                  .AlignCenter()
                                  .Image(imagePath)
                                  .FitWidth();

                            column.Item().PaddingVertical(15);
                        }
                    }

                    // متن مقاله
                    column.Item().Text(FixRtlText(article.Description))
                        .FontSize(12)
                        .LineHeight(1.7f)
                        .AlignRight()
                        .DirectionFromRightToLeft();
                });
            });
        });

        return document.GeneratePdf();
    }

    private static string FixRtlText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        return "\u202B" + text + "\u202C";
    }
}
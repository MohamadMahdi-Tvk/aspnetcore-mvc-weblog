using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.DTOs.Articles;
using WeblogSample.Service.Interfaces;

namespace WeblogSample.WebApp.Components;

public class PopularArticle : ViewComponent
{
    private readonly IArticleService _articleService;

    public PopularArticle(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var popularArticle = await _articleService.GetAllPopularAsync();

        var list = popularArticle ?? new List<ArticleListDto>();

        return View(list);
    }
}

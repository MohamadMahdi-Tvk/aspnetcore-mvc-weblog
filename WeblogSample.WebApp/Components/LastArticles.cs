using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.Interfaces;

namespace WeblogSample.WebApp.Components;

public class LastArticles : ViewComponent
{
    private readonly IArticleService _articleService;

    public LastArticles(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int page = 1)
    {
        const int pageSize = 6;

        var result = await _articleService.GetLatestArticlesAsync(page, pageSize);

        return View(result);
    }
}
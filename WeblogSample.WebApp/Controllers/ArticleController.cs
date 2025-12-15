using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeblogSample.Service.DTOs.Comments;
using WeblogSample.Service.Interfaces;
using WeblogSample.WebApp.Infra.PdfGenerator;
using WeblogSample.WebApp.Models;

namespace WeblogSample.WebApp.Controllers;

public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICommentService _commentService;
    private readonly IArticlePdfGenerator _pdfGenerator;

    public ArticleController(IArticleService articleService, ICommentService commentService,IArticlePdfGenerator pdfGenerator)
    {
        _articleService = articleService;
        _commentService = commentService;
        _pdfGenerator = pdfGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> ShowArticle(long id)
    {
        var article = await _articleService.GetByIdAsync(id);

        return View(article);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(ShowArticle), new { id = model.ArticleId });

        long personId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var comment = new AddCommentDto
        {
            ArticleId = model.ArticleId,
            PersonId = personId,
            Text = model.Text
        };

        await _commentService.AddAsync(comment);

        return RedirectToAction(nameof(ShowArticle), new { id = model.ArticleId });
    }

    [HttpGet]
    public async Task<IActionResult> ShowArticlesByCategoryId(short categoryId, int page = 1)
    {
        ViewBag.PageNumber = page;
        ViewBag.CategoryId = categoryId;

        const int pageSize = 5;

        var articles = await _articleService.GetArticlesByCategoryId(page, pageSize, categoryId);

        return View(articles);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadPdf(long id)
    {

        var article = await _articleService.GetByIdAsync(id);

        if (article == null)
            return NotFound();

        var pdfBytes = _pdfGenerator.Generate(article);

        return File(pdfBytes, "application/pdf", $"{article.Title}.pdf");
    }
}

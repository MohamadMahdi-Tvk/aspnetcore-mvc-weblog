using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeblogSample.Service.DTOs.Comments;
using WeblogSample.Service.Interfaces;
using WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Comments;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> CommentList(long id)
    {
        return View(await _commentService.GetAllByArticleIdAsync(id));
    }

    [HttpGet]
    public IActionResult ReplayComment(long parentId, long articleId, long personId)
    {
        var model = new CreateCommentViewModel
        {
            ArticleId = articleId,
            ParentId = parentId,
            PersonId = personId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ReplayComment(CreateCommentViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        long personId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var dto = new CommentReplayDto
        {
            ArticleId = model.ArticleId,
            ParentId = model.ParentId,
            Text = model.Text,
            PersonId = personId,
            IsApproved = true,
            InsertDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };

        await _commentService.ReplayComment(dto);

        return RedirectToAction("CommentList", "Comment",
            new { id = model.ArticleId });
    }

    [HttpGet]
    public async Task<IActionResult> RemoveComment(long id, long articleId)
    {
        await _commentService.DeleteAsync(id);

        return RedirectToAction("CommentList", "Comment", new { id = articleId });
    }
}

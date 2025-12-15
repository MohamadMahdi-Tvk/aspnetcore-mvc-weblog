using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeblogSample.Service.DTOs.Articles;
using WeblogSample.Service.DTOs.Categories;
using WeblogSample.Service.Interfaces;
using WeblogSample.Service.Services;
using WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Articles;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;


public class ArticleController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _env;

    public ArticleController(IArticleService articleService,
        ICategoryService categoryService,
        IWebHostEnvironment env)
    {
        _articleService = articleService;
        _categoryService = categoryService;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> ArticleList()
    {
        return View(await _articleService.GetAllAsync());
    }


    [HttpGet]
    public async Task<IActionResult> CreateArticle()
    {
        var categories = await _categoryService.GetAllAsync();

        ViewBag.Categories = categories;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            string imagesFolder = Path.Combine(_env.WebRootPath, "images");

            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);

            string filePath = Path.Combine(imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            model.ImagePath = "/images/" + fileName;
        }

        long personId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


        var newArticle = new ArticleCreateDto
        {
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            ImagePath = model.ImagePath,
            CategoryId = model.CategoryId,
            PersonId = personId,
            VisitCount = model.VisitCount
        };

        await _articleService.CreateAsync(newArticle);

        return RedirectToAction(nameof(ArticleList));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateArticle(long id)
    {
        var categories = await _categoryService.GetAllAsync();
        var article = await _articleService.GetByIdAsync(id);

        ViewBag.Categories = categories;

        return View(new UpdateArticleViewModel
        {
            Id = article.Id,
            Title = article.Title,
            CategoryId = article.CategoryId,
            ShortDescription = article.ShortDescription,
            Description = article.Description,
            PersonId = article.PersonId,
            ImagePath = article.ImagePath,
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateArticle(UpdateArticleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(model);
        }

        var existingArticle = await _articleService.GetByIdAsync(model.Id);

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            string imagesFolder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
            string filePath = Path.Combine(imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(existingArticle.ImagePath))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, existingArticle.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            model.ImagePath = "/images/" + fileName;
        }
        else
        {
            model.ImagePath = existingArticle.ImagePath;
        }

        var updatedArticle = new ArticleUpdateDto
        {
            Id = model.Id,
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            ImagePath = model.ImagePath,
            CategoryId = model.CategoryId,
            PersonId = model.PersonId,
        };

        await _articleService.UpdateAsync(updatedArticle);

        return RedirectToAction(nameof(ArticleList));
    }


    [HttpGet]
    public async Task<IActionResult> ToggleArticleStatus(long id)
    {
        await _articleService.ToggleArticleAsync(id);

        return RedirectToAction(nameof(ArticleList));
    }

}
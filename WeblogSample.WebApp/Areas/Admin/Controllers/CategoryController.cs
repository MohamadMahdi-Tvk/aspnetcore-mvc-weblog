using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.DTOs.Categories;
using WeblogSample.Service.Interfaces;
using WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Categories;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> ShowCategories()
    {

        return View(await _categoryService.GetAllAsync());
    }

    [HttpGet]
    public async Task<IActionResult> CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var newCategory = new CategoryDto
        {
            Name = model.Name,
        };

        await _categoryService.CreateAsync(newCategory);

        return RedirectToAction(nameof(ShowCategories));
    }


    [HttpGet]
    public async Task<IActionResult> UpdateCategory(short id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        return View(new UpdateCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);

        await _categoryService.UpdateAsync(new CategoryDto
        {
            Id = model.Id,
            Name = model.Name,
        });

        return RedirectToAction(nameof(ShowCategories));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveCategory(short id)
    {
        await _categoryService.DeleteAsync(id);

        return RedirectToAction(nameof(ShowCategories));
    }

}
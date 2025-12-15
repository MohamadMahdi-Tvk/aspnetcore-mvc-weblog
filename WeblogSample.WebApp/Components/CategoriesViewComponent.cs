using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.Interfaces;

namespace WeblogSample.WebApp.Components;

public class CategoriesViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoriesViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
       var categories = await _categoryService.GetAllAsync();

        return View(categories);
    }
}

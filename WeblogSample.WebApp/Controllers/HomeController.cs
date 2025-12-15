using Microsoft.AspNetCore.Mvc;

namespace WeblogSample.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(int page = 1)
    {
        ViewBag.PageNumber = page;

        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }
}

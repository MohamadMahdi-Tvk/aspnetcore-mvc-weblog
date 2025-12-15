using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeblogSample.Data.Contexts;
using WeblogSample.Data.Entities;
using WeblogSample.WebApp.Infra.JWTGenerator;
using WeblogSample.WebApp.Infra.PasswordHashing;
using WeblogSample.WebApp.Models;

namespace WeblogSample.WebApp.Controllers;

public class AccountController : Controller
{
    private readonly DataBaseContext _context;
    private readonly IJwtService _jwtService;

    public AccountController(DataBaseContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var exists = _context.People
            .Any(p => p.UserName == model.UserName);

        if (exists)
        {
            ModelState.AddModelError("", "این نام کاربری قبلاً ثبت شده است");
            return View(model);
        }

        var user = new Person
        {
            UserName = model.UserName,
            PasswordHash = PasswordHasher.Hash(model.Password),
            RoleId = 2
        };

        _context.People.Add(user);
        _context.SaveChanges();

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.People
            .Include(p => p.Role)
            .SingleOrDefault(p =>
                p.UserName == model.UserName);
 

        if (user == null ||
            !PasswordHasher.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError("", "نام کاربری یا رمز عبور نادرست است");
            return View(model);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError("", "دسترسی شما به وبلاگ مسدود شده است");
            return View(model);
        }

        var token = _jwtService.GenerateToken(user);

        Response.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.Now.AddMinutes(60)
        });

        return RedirectToAction("Index", "Home");
    }


    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;

[Area("admin")]
[Authorize(Roles = "Admin")]
public abstract class BaseController : Controller
{
}

using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.DTOs.Roles;
using WeblogSample.Service.Interfaces;
using WeblogSample.WebApp.Areas.Admin.Models.ViewModels.Roles;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> ShowRoles()
    {       
        return View(await _roleService.GetAllAsync());
    }

    [HttpGet]
    public async Task<IActionResult> CreateRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var newRole = new RoleDto
        {
            Name = model.Name
        };

        await _roleService.CreateAsync(newRole);

        return Redirect(nameof(ShowRoles));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateRole(short id)
    {
        var role = await _roleService.GetByIdAsync(id);


        return View(new UpdateRoleViewModel
        {
            Id = id,
            Name = role.Name
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRole(UpdateRoleViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _roleService.UpdateAsync(new RoleDto
        {
            Id = model.Id,
            Name = model.Name
        });

        return RedirectToAction(nameof(ShowRoles));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveRole(short id)
    {
        await _roleService.DeleteAsync(id);

        return RedirectToAction(nameof(ShowRoles));
    }
}

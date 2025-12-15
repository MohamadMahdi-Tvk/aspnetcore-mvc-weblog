using Microsoft.AspNetCore.Mvc;
using WeblogSample.Service.DTOs.Persons;
using WeblogSample.Service.Interfaces;
using WeblogSample.WebApp.Areas.Admin.Models.ViewModels.People;

namespace WeblogSample.WebApp.Areas.Admin.Controllers;

public class PeopleController : BaseController
{
    private readonly IPersonService _personService;
    private readonly IRoleService _roleService;

    public PeopleController(IPersonService personService, IRoleService roleService)
    {
        _personService = personService;
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> PeopleList()
    {
        var people = await _personService.GetAllAsync();

        return View(people);
    }

    [HttpGet]
    public async Task<IActionResult> CreatePerson()
    {
        var roleList = await _roleService.GetAllAsync();

        ViewBag.Roles = roleList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _roleService.GetAllAsync();

            return View(model);
        }

        var personDto = new PersonCreateDto
        {
            UserName = model.UserName,
            Password = model.Password,
            RoleId = model.RoleId
        };

        await _personService.CreateAsync(personDto);

        return RedirectToAction("PeopleList", "People", new { area = "Admin" });
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePerson(long id)
    {
        var roleList = await _roleService.GetAllAsync();

        ViewBag.Roles = roleList;

        var person = await _personService.GetByIdAsync(id);

        return View(new UpdatePersonViewModel
        {
            Id = person.Id,
            UserName = person.UserName,
            RoleId = person.RoleId
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePerson(UpdatePersonViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await _roleService.GetAllAsync();

            return View(model);
        }

        var updatedPerson = new PersonUpdateDto
        {
            Id = model.Id,
            RoleId = model.RoleId,
            UserName = model.UserName,
        };


        await _personService.UpdateAsync(updatedPerson);

        return RedirectToAction("PeopleList", "People", new { area = "Admin" });

    }

    [HttpGet]
    public async Task<IActionResult> ToggleUserStatus(long id)
    {
        await _personService.ToggleActiveAsync(id);

        return RedirectToAction(nameof(PeopleList));
    }
}
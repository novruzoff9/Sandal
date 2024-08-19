using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var allusers = _userManager.Users.ToList();
        var userlist = new List<UserListDto>();
        foreach (var item in allusers)
        {
            UserListDto userlistDto = _mapper.Map<UserListDto>(item);
            userlistDto.Roles = string.Join(",", _userManager.GetRolesAsync(item).Result.ToArray());
            userlist.Add(userlistDto);
        }
        return View(userlist);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return BadRequest();
        }
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return BadRequest();
        }
        var userdetails = _mapper.Map<UserDetailsDto>(user);
        userdetails.Roles = string.Join(",", _userManager.GetRolesAsync(user).Result.ToArray());
        return View(userdetails);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateDto userDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    string defaultrole = "user";
                    if (!await _roleManager.RoleExistsAsync(defaultrole))
                    {
                        await _roleManager.CreateAsync(new Role { Name = defaultrole });
                    }
                    await _userManager.AddToRoleAsync(user, defaultrole);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View(userDto);
            }
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public async Task<IActionResult> Edit(string id)
    {
        return View(await _userManager.FindByIdAsync(id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

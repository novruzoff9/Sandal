using AutoMapper;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using EntityLayer.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly SandalContext _context;
    private readonly IMapper _mapper;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, SandalContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        AccountViewModel model = new AccountViewModel();
        if (TempData["logintry"] != null)
        {
            var login = JsonConvert.DeserializeObject<UserLoginDto>(TempData["logintry"].ToString());
            ModelState.AddModelError("Password", "İstifadəçi adı və ya şifrə yanlışdır");
            model.Login = login;
        }
        else if (TempData["CreatedUser"] != null)
        {
            var login = JsonConvert.DeserializeObject<UserLoginDto>(TempData["CreatedUser"].ToString());
            model.Login = login;
        }
        else if (TempData["registertry"] != null)
        {
            var register = JsonConvert.DeserializeObject<UserRegisterDto>(TempData["registertry"].ToString());
            model.Register = register;
            model.IsRegistering = true;
        }
        return View(model);
    }

    public IActionResult SignIn()
    {
        return PartialView();
    }
    [HttpPost]
    public async Task<IActionResult> SignIn(UserLoginDto login, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
            {
                TempData["logintry"] = JsonConvert.SerializeObject(login);
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (result.Succeeded)
            {
                string decodeUrl = Uri.UnescapeDataString(returnUrl ?? @"\");

                CookieOptions options = new()
                {
                    Expires = DateTime.Now.AddMonths(1)
                };

                Response.Cookies.Append("CURRENT_USER", JsonConvert.SerializeObject(user), options);
                return Redirect(decodeUrl);
            }
        }
        TempData["logintry"] = JsonConvert.SerializeObject(login);
        return RedirectToAction(nameof(Login));
    }

    public IActionResult Register()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDto register, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(register);
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                TempData["CreatedUser"] = JsonConvert.SerializeObject(register);
                return RedirectToAction(nameof(Login));
            }
            else
            {
                TempData["registertry"] = JsonConvert.SerializeObject(register);
                return RedirectToAction(nameof(Login));
            }
        }
        TempData["registertry"] = JsonConvert.SerializeObject(register);
        return RedirectToAction(nameof(Login));
    }

    public IActionResult _ProfileMenu()
    {
        return PartialView();
    }

    public IActionResult Profile()
    {
        var user = JsonConvert.DeserializeObject<User>(Request.Cookies["CURRENT_USER"]);
        return View(user);
    }

    public async Task<IActionResult> UpdateProfile(UserUpdateProfileDto user)
    {
        var currentuser = await _userManager.GetUserAsync(User);
        foreach (var property in typeof(UserUpdateProfileDto).GetProperties())
        {
            if (property.GetValue(user) != default && property.GetValue(user) != null)
            {
                typeof(User).GetProperty(property.Name).SetValue(currentuser, property.GetValue(user));
            }
        }
        await _userManager.UpdateAsync(currentuser);
        _context.SaveChanges();

        Response.Cookies.Delete("CURRENT_USER"); 

        CookieOptions options = new()
        {
            Expires = DateTime.Now.AddMonths(1)
        };

        Response.Cookies.Append("CURRENT_USER", JsonConvert.SerializeObject(currentuser), options);
        return RedirectToAction(nameof(Profile));
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        Response.Cookies.Delete("CURRENT_USER");
        return RedirectToAction(nameof(Login));
    }
}

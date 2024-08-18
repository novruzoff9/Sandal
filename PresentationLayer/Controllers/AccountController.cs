﻿using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
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
        await _signInManager.SignOutAsync();
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user is null)
            {
                TempData["logintry"] = JsonConvert.SerializeObject(login);
                return RedirectToAction(nameof(Login));
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);

            if (result.Succeeded)
            {
                string decodeUrl = Uri.UnescapeDataString(returnUrl ?? @"\");
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
}

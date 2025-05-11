using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using APDS_POE.Models;
using APDS_POE.Models.System;
using Microsoft.AspNetCore.Authorization;

namespace APDS_POE.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult FarmerLogin()
    {
        return View("~/Views/Farmer/FarmerLogin.cshtml");
    }

    [AllowAnonymous]
    public IActionResult EmployeeLogin()
    {
        return View("~/Views/Employee/EmployeeLogin.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}

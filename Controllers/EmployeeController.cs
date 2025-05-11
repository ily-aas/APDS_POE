using APDS_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View("E_Dashboard");
        }
    }
}

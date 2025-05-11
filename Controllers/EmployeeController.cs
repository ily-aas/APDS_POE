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

        [HttpGet]
        public IActionResult AddFarmer()
        {

            return View("AddFarmer");
        }

        [HttpPost]
        public IActionResult AddFarmer(User user)
        {
            return Ok();
        }
    }
}

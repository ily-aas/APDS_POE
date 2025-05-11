using APDS_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return View("F_Dashboard");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View("AddProduct");
        }

        [HttpPost]
        public IActionResult AddProduct(Product Product)
        {
            return Ok();
        }
    }
}

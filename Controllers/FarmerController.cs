using APDS_POE.Models;
using APDS_POE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IProductRepository _productRepo;

        public FarmerController(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult Index()
        {
            return View("F_Dashboard");
        }

        [HttpGet]
        [Authorize(Roles = "Farmer")]
        public IActionResult Product(Product product)
        {
            return View("AddProduct",product);
        }

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public IActionResult AddProduct(Product Product)
        {
            if (Product == null)
                return BadRequest();

            var Result = _productRepo.AddProduct(Product);

            Product.HasErrors = Result.IsSuccess ? false : true;
            Product.Message = Result.Message;

            return RedirectToAction("Product",Product);
        }
    }
}

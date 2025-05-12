using APDS_POE.Models;
using APDS_POE.Repositories;
using APDS_POE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class FarmerController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IHelperService _helper;

        public FarmerController(IProductRepository productRepository, IHelperService helperService)
        {
            _productRepo = productRepository;
            _helper = helperService;
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult Index()
        {
            var user = _helper.GetSignedInUser();
            var products = _productRepo.GetProducts(user.Id);
            return View("F_Dashboard",products);
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

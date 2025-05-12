using APDS_POE.Models;
using APDS_POE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static APDS_POE.Models.System.Dtos;

namespace APDS_POE.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _prodRepo;

        public EmployeeController(IUserRepository userRepository, IProductRepository productRepo)
        {
            _userRepo = userRepository;
            _prodRepo = productRepo;
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Index()
        {
            var users = _userRepo.GetAllUsers();
            return View("E_Dashboard",users);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Farmer(User user)
        {
            return View("AddFarmer",user);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult RenderPartial(int Type)
        {
            switch (Type)
            {
                case 1:
                    var Product = _prodRepo.GetAllProducts();
                    return PartialView("_filteredProduct",Product);
                case 2:
                    var users = _userRepo.GetAllUsers();
                    return PartialView("_Product",users);
                default:
                    return BadRequest("Invalid type specified.");

            }
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public IActionResult AddFarmer(User user)
        {
            if (user == null)
                return BadRequest(user);

            var result = _userRepo.AddUser(user);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult GetFilteredProducts(ProductFilterDto Dto)
        {

            List<Product> products = new List<Product>();

            if (Dto.DateFilter == Models.System.Enums.DateFilter.All && (Dto.Date == null || Dto.Date == DateTime.MinValue) && Dto.Category == Models.System.Enums.Category.All)
            {
                products = _prodRepo.GetAllProducts();
                return Ok(new
                {
                    products = products,
                });
            }

            products = _prodRepo.GetFilteredProducts(Dto);

            if (products.Count > 0)
                return Ok(new
                {
                    products = products,
                });

            return BadRequest("No Products Found");
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult ViewFarmerProducts(int ID)
        {
            if (ID == null || ID == 0)
                return BadRequest();

            var User = _userRepo.GetUser(ID);
            var Products = _prodRepo.GetProducts(ID);

            if(User == null)
                return BadRequest();

            return Ok(new { 
            
                User = User,
                Products = Products

            });

        }
    }
}

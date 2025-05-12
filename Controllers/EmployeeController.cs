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
        public IActionResult ViewProducts(int ID)
        {
            if (ID == null || ID == 0)
                return BadRequest();

            var User = _userRepo.GetUser(ID);
            var Products = _prodRepo.GetProducts(ID);

            if(User == null)
                return BadRequest();

            return RedirectToPage("~/Views/Products/Product.cshtml", new ProductVM()
            {
                user = User,
                Products = Products
            });

        }
    }
}

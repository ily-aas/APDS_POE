using APDS_POE.Models;
using APDS_POE.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IUserRepository _userRepo;
        public EmployeeController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public IActionResult Index()
        {
            return View("E_Dashboard");
        }

        [HttpGet]
        public IActionResult Farmer(User user)
        {

            return View("AddFarmer",user);
        }

        [HttpPost]
        public IActionResult AddFarmer(User user)
        {
            if (user == null)
                return BadRequest(user);

            var result = _userRepo.AddUser(user);

            return Ok();
        }
    }
}

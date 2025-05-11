using APDS_POE.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APDS_POE.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserRepository Repo;

        public AccountController(IUserRepository userRepository)
        {
            Repo = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password,bool IsAdmin)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest();

            var response = Repo.Login(username,password,IsAdmin);

            if (response.IsSuccess && IsAdmin)
                return RedirectToAction("Index","Employee");

            if (response.IsSuccess && !IsAdmin)
                return RedirectToAction("Index", "Farmer");

            ViewBag.Error = "Invalid username or password";
            return View();
        }
    }
}

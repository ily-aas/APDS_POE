using APDS_POE.Repositories;
using APDS_POE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static APDS_POE.Models.System.Enums;

namespace APDS_POE.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserRepository Repo;
        private readonly JwtAuthentication _auth;

        public AccountController(IUserRepository userRepository, JwtAuthentication jwtAuthentication)
        {
            Repo = userRepository;
            _auth = jwtAuthentication;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string username, string password,bool IsAdmin)
        {

            UserRole Role = IsAdmin ? UserRole.Employee : UserRole.Farmer;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest();

            var response = Repo.Login(username,password,IsAdmin);

            //Generate the token
            var token = _auth.GenerateToken(username, Role);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // set to true if you're using HTTPS
                Expires = DateTimeOffset.UtcNow.AddHours(1),
                SameSite = SameSiteMode.Strict // Optional: restrict cross-site sending
            };

            Response.Cookies.Append("jwt_token", token, cookieOptions);

            if (response.IsSuccess && IsAdmin)
                return RedirectToAction("Index","Employee");

            if (response.IsSuccess && !IsAdmin)
                return RedirectToAction("Index", "Farmer");

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt_token");
            return RedirectToAction("Index","Home");
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Models.Domain;
using System.Diagnostics;

namespace ProyectoLenguajes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager)
        {
            this._logger = logger;
            this._userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EditProfile()
        {
            return View();
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult ChangeUsername()
        {
            return View();
        }

        public IActionResult ChangeEmail()
        {
            return View();
        }

        public async Task<IActionResult> ViewDetail()
        {
            // Obtener el usuario actualmente autenticado
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // Manejar el caso cuando el usuario no se encuentra
                return NotFound();
            }

            // Crear el modelo con los datos del usuario
            var userModel = new User
            {
                username = user.UserName,
                email = user.Email,
                tipo = "Algun tipo" // Reemplaza esto con el tipo real del usuario
            };

            // Pasar el modelo a la vista
            return View(userModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
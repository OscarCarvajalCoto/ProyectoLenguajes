using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models.Domain;
using ProyectoLenguajes.Models;

namespace ProyectoLenguajes.Controllers
{
    public class ChangeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChangeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager)
        {
            this._logger = logger;
            this._userManager = _userManager;
        }

        public IActionResult Index()
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

        public async Task<IActionResult> ChangeProfilePicture()
        {
            // Obtener el usuario actualmente autenticado
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                string profilePictureBase64 = user.ProfilePicture;
                // Verificar si la imagen de perfil existe
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    // Convertir la cadena base64 en una URL válida
                    string imageFormat = "image/png"; // Cambia esto según el formato de imagen que estés utilizando
                    string profilePictureUrl = $"data:{imageFormat};base64,{profilePictureBase64}";
                    // Pasar la URL de la imagen de perfil a la vista
                    ViewBag.ProfilePicture = profilePictureUrl;
                }
                else
                {
                    ViewBag.ProfilePicture = "https://www.kindpng.com/picc/m/24-248253_user-profile-default-image-png-clipart-png-download.png";
                }
            }

            return View();
        }

        public async Task<IActionResult> ViewDetail()
        {
            var ret = new EditUser();
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

            ret.user = userModel;

            ApplicationUser userAux = await _userManager.GetUserAsync(HttpContext.User);

            if (userAux != null)
            {

                string profilePictureBase64 = user.ProfilePicture;
                // Verificar si la imagen de perfil existe
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    // Convertir la cadena base64 en una URL válida
                    string imageFormat = "image/png"; // Cambia esto según el formato de imagen que estés utilizando
                    string profilePictureUrl = $"data:{imageFormat};base64,{profilePictureBase64}";
                    // Pasar la URL de la imagen de perfil a la vista
                    var AppUser = new ApplicationUser
                    {
                        Name = userAux.UserName,
                        ProfilePicture = profilePictureUrl
                    };
                    ret.applicationUser = AppUser;


                }
                else
                {
                    var AppUser = new ApplicationUser
                    {
                        Name = userAux.UserName,
                        ProfilePicture = "https://www.kindpng.com/picc/m/24-248253_user-profile-default-image-png-clipart-png-download.png"
                    };
                    ret.applicationUser = AppUser;

                }

            }
            // Pasar el modelo a la vista
            return View(ret);
        }
    }
}

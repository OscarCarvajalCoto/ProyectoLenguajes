using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Data;
using ProyectoLenguajes.Models;
using ProyectoLenguajes.Models.Domain;
using System;
using System.Diagnostics;
using System.Net;

namespace ProyectoLenguajes.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDataContext db = new ApplicationDataContext();
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> _userManager, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._userManager = _userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var lists = new HomePageModel();

            lists.last_ten = db.Movie_Series.ToList();
            //ahora mismo los tres carouseles van a ser iguales
            lists.carrousel_1 = db.Movie_Series.ToList(); 
            lists.carrousel_2 = db.Movie_Series.ToList();
            lists.carrousel_3 = db.Movie_Series.ToList();




            //escoger generos y escogerlos de manera aleatoria
            var genre = db.Genres.ToList();
            var lisGenreAux = new List<string>();

            foreach (var item in genre) {
                lisGenreAux.Add(item.genre_name);    
            }


            Random random = new Random();
            List<string> generosAleatorios = lisGenreAux.OrderBy(x => random.Next()).Distinct().Take(3).ToList();


            
            lists.genre1 = generosAleatorios.ElementAtOrDefault(0);
            lists.genre2 = generosAleatorios.ElementAtOrDefault(1);
            lists.genre3 = generosAleatorios.ElementAtOrDefault(2);
            
            //conseguir imagen de perfil



            return View(lists);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> UserPhoto()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            byte[] profilePictureBytes;

            if (user != null && !string.IsNullOrEmpty(user.ProfilePicture))
            {
                // Convertir la cadena base64 en una matriz de bytes
                profilePictureBytes = Convert.FromBase64String(user.ProfilePicture);
            }
            else
            {
                // Cargar una imagen predeterminada desde una URL web
                string defaultImageUrl = "https://www.kindpng.com/picc/m/24-248253_user-profile-default-image-png-clipart-png-download.png";
                using (var webClient = new WebClient())
                {
                    profilePictureBytes = await webClient.DownloadDataTaskAsync(defaultImageUrl);
                }
            }

            // Retornar la imagen de perfil como un archivo de imagen
            return new FileContentResult(profilePictureBytes, "image/png");
        }
                
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
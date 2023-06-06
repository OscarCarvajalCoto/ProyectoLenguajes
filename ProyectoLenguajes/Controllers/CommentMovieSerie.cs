using Microsoft.AspNetCore.Mvc;

namespace ProyectoLenguajes.Controllers
{
    public class CommentMovieSerie : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Comment()
        {
            return View();
        }
    }
}
